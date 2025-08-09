using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            return Ok(new { token, user = new { user.Email, user.UserName } });
        }

        return Unauthorized("Invalid credentials");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Username + "@example.com",
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("login-google")]
    public IActionResult LoginGoogle(string returnUrl = null)
    {
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
            Url.Action(nameof(ExternalLoginCallback), new { returnUrl }));
        return Challenge(properties, "Google");
    }

    [HttpGet("login-github")]
    public IActionResult LoginGitHub(string returnUrl = null)
    {
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("GitHub",
            Url.Action(nameof(ExternalLoginCallback), new { returnUrl }));
        return Challenge(properties, "GitHub");
    }

    [HttpGet("external-login-callback")]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return BadRequest("Error loading external login information");
        }


        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            var token = GenerateJwtToken(user);

            var frontendUrl = returnUrl ?? "http://localhost:4200";
            return Redirect($"{frontendUrl}#token={token}");
        }


        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);

        if (email != null)
        {
            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser);
            if (createResult.Succeeded)
            {
                await _userManager.AddLoginAsync(newUser, info);
                var token = GenerateJwtToken(newUser);

                var frontendUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "http://localhost:4200";
                return Redirect($"{frontendUrl}#token={token}");
            }

            return BadRequest(createResult.Errors);
        }

        return BadRequest("Unable to create user from external login");
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logged out successfully" });
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


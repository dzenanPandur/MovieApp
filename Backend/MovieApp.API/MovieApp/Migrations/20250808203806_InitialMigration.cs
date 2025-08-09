using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Cast", "CoverImage", "ReleaseDate", "ShortDescription", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "", "shawshank.jpg", new DateTime(1994, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Two imprisoned men bond over a number of years.", "The Shawshank Redemption", 0 },
                    { 2, "", "godfather.jpg", new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.", "The Godfather", 0 },
                    { 3, "", "darkknight.jpg", new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Batman faces the Joker, a criminal mastermind.", "The Dark Knight", 0 },
                    { 4, "", "godfather2.jpg", new DateTime(1974, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The early life and career of Vito Corleone.", "The Godfather Part II", 0 },
                    { 5, "", "12angrymen.jpg", new DateTime(1957, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A jury holdout attempts to prevent a miscarriage of justice.", "12 Angry Men", 0 },
                    { 6, "", "schindler.jpg", new DateTime(1993, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "In German-occupied Poland during WWII, Oskar Schindler becomes concerned for his Jewish workforce.", "Schindler's List", 0 },
                    { 7, "", "lotr3.jpg", new DateTime(2003, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gandalf and Aragorn lead the World of Men against Sauron's army.", "The Lord of the Rings: The Return of the King", 0 },
                    { 8, "", "pulpfiction.jpg", new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The lives of two mob hitmen, a boxer, and others intertwine.", "Pulp Fiction", 0 },
                    { 9, "", "lotr1.jpg", new DateTime(2001, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "A meek Hobbit sets out on a journey to destroy the One Ring.", "The Lord of the Rings: The Fellowship of the Ring", 0 },
                    { 10, "", "forrestgump.jpg", new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "The presidencies of Kennedy and Johnson, the Vietnam War, and more through the eyes of Forrest Gump.", "Forrest Gump", 0 },
                    { 11, "", "fightclub.jpg", new DateTime(1999, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "An insomniac office worker forms an underground fight club.", "Fight Club", 0 },
                    { 12, "", "inception.jpg", new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "A thief who steals corporate secrets through dream-sharing technology.", "Inception", 0 },
                    { 13, "", "lotr2.jpg", new DateTime(2002, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "While Frodo and Sam edge closer to Mordor, the divided fellowship makes a stand.", "The Lord of the Rings: The Two Towers", 0 },
                    { 14, "", "empirestrikesback.jpg", new DateTime(1980, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "After the rebels are brutally overpowered, Luke begins Jedi training.", "Star Wars: Episode V - The Empire Strikes Back", 0 },
                    { 15, "", "matrix.jpg", new DateTime(1999, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "A computer hacker learns about the true nature of reality.", "The Matrix", 0 },
                    { 16, "", "breakingbad.jpg", new DateTime(2008, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "A high school chemistry teacher turned methamphetamine producer.", "Breaking Bad", 1 },
                    { 17, "", "got.jpg", new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nine noble families wage war against each other in Westeros.", "Game of Thrones", 1 },
                    { 18, "", "thewire.jpg", new DateTime(2002, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baltimore drug scene, seen through the eyes of law enforcement and drug dealers.", "The Wire", 1 },
                    { 19, "", "strangerthings.jpg", new DateTime(2016, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A group of young friends witness supernatural forces and secret government exploits.", "Stranger Things", 1 },
                    { 20, "", "sopranos.jpg", new DateTime(1999, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Jersey mob boss Tony Soprano deals with personal and professional issues.", "The Sopranos", 1 },
                    { 21, "", "sherlock.jpg", new DateTime(2010, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.", "Sherlock", 1 },
                    { 22, "", "friends.jpg", new DateTime(1994, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Follows the personal and professional lives of six twenty to thirty-something friends.", "Friends", 1 },
                    { 23, "", "theoffice.jpg", new DateTime(2005, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "A mockumentary on a group of typical office workers.", "The Office", 1 },
                    { 24, "", "bettercallsaul.jpg", new DateTime(2015, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "The trials and tribulations of criminal lawyer Jimmy McGill.", "Better Call Saul", 1 },
                    { 25, "", "houseofcards.jpg", new DateTime(2013, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Congressman works with his equally conniving wife to exact revenge.", "House of Cards", 1 },
                    { 26, "", "chernobyl.jpg", new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "A dramatization of the true story of the Chernobyl nuclear disaster.", "Chernobyl", 1 },
                    { 27, "", "mandalorian.jpg", new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lone gunfighter makes his way through the outer reaches of the galaxy.", "The Mandalorian", 1 },
                    { 28, "", "lost.jpg", new DateTime(2004, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "The survivors of a plane crash are forced to work together.", "Lost", 1 },
                    { 29, "", "truedetective.jpg", new DateTime(2014, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seasonal anthology series in which police investigations unearth personal and professional secrets.", "True Detective", 1 },
                    { 30, "", "seinfeld.jpg", new DateTime(1989, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "The continuing misadventures of neurotic New York stand-up comedian Jerry Seinfeld.", "Seinfeld", 1 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 2, 1, 5 },
                    { 3, 1, 5 },
                    { 4, 1, 5 },
                    { 5, 1, 5 },
                    { 6, 1, 5 },
                    { 7, 1, 5 },
                    { 8, 1, 5 },
                    { 9, 1, 5 },
                    { 10, 1, 4 },
                    { 11, 2, 5 },
                    { 12, 2, 5 },
                    { 13, 2, 5 },
                    { 14, 2, 5 },
                    { 15, 2, 5 },
                    { 16, 2, 5 },
                    { 17, 2, 5 },
                    { 18, 2, 5 },
                    { 19, 2, 4 },
                    { 20, 2, 4 },
                    { 21, 3, 5 },
                    { 22, 3, 5 },
                    { 23, 3, 5 },
                    { 24, 3, 5 },
                    { 25, 3, 5 },
                    { 26, 3, 5 },
                    { 27, 3, 5 },
                    { 28, 3, 4 },
                    { 29, 3, 4 },
                    { 30, 3, 4 },
                    { 31, 4, 5 },
                    { 32, 4, 5 },
                    { 33, 4, 5 },
                    { 34, 4, 5 },
                    { 35, 4, 5 },
                    { 36, 4, 5 },
                    { 37, 4, 4 },
                    { 38, 4, 4 },
                    { 39, 4, 4 },
                    { 40, 4, 4 },
                    { 41, 5, 5 },
                    { 42, 5, 5 },
                    { 43, 5, 5 },
                    { 44, 5, 5 },
                    { 45, 5, 5 },
                    { 46, 5, 4 },
                    { 47, 5, 4 },
                    { 48, 5, 4 },
                    { 49, 5, 4 },
                    { 50, 5, 4 },
                    { 51, 6, 5 },
                    { 52, 6, 5 },
                    { 53, 6, 5 },
                    { 54, 6, 5 },
                    { 55, 6, 4 },
                    { 56, 6, 4 },
                    { 57, 6, 4 },
                    { 58, 6, 4 },
                    { 59, 6, 4 },
                    { 60, 6, 4 },
                    { 61, 7, 5 },
                    { 62, 7, 5 },
                    { 63, 7, 5 },
                    { 64, 7, 4 },
                    { 65, 7, 4 },
                    { 66, 7, 4 },
                    { 67, 7, 4 },
                    { 68, 7, 4 },
                    { 69, 7, 4 },
                    { 70, 7, 4 },
                    { 71, 8, 5 },
                    { 72, 8, 5 },
                    { 73, 8, 4 },
                    { 74, 8, 4 },
                    { 75, 8, 4 },
                    { 76, 8, 4 },
                    { 77, 8, 4 },
                    { 78, 8, 4 },
                    { 79, 8, 4 },
                    { 80, 8, 4 },
                    { 81, 9, 5 },
                    { 82, 9, 4 },
                    { 83, 9, 4 },
                    { 84, 9, 4 },
                    { 85, 9, 4 },
                    { 86, 9, 4 },
                    { 87, 9, 4 },
                    { 88, 9, 4 },
                    { 89, 9, 4 },
                    { 90, 9, 4 },
                    { 91, 10, 4 },
                    { 92, 10, 4 },
                    { 93, 10, 4 },
                    { 94, 10, 4 },
                    { 95, 10, 4 },
                    { 96, 10, 4 },
                    { 97, 10, 4 },
                    { 98, 10, 4 },
                    { 99, 10, 4 },
                    { 100, 10, 4 },
                    { 101, 11, 4 },
                    { 102, 11, 4 },
                    { 103, 11, 4 },
                    { 104, 11, 4 },
                    { 105, 11, 4 },
                    { 106, 11, 4 },
                    { 107, 11, 4 },
                    { 108, 11, 4 },
                    { 109, 11, 4 },
                    { 110, 11, 3 },
                    { 111, 12, 4 },
                    { 112, 12, 4 },
                    { 113, 12, 4 },
                    { 114, 12, 4 },
                    { 115, 12, 4 },
                    { 116, 12, 4 },
                    { 117, 12, 4 },
                    { 118, 12, 4 },
                    { 119, 12, 3 },
                    { 120, 12, 3 },
                    { 121, 13, 4 },
                    { 122, 13, 4 },
                    { 123, 13, 4 },
                    { 124, 13, 4 },
                    { 125, 13, 4 },
                    { 126, 13, 4 },
                    { 127, 13, 4 },
                    { 128, 13, 3 },
                    { 129, 13, 3 },
                    { 130, 13, 3 },
                    { 131, 14, 4 },
                    { 132, 14, 4 },
                    { 133, 14, 4 },
                    { 134, 14, 4 },
                    { 135, 14, 4 },
                    { 136, 14, 4 },
                    { 137, 14, 3 },
                    { 138, 14, 3 },
                    { 139, 14, 3 },
                    { 140, 14, 3 },
                    { 141, 15, 4 },
                    { 142, 15, 4 },
                    { 143, 15, 4 },
                    { 144, 15, 4 },
                    { 145, 15, 4 },
                    { 146, 15, 3 },
                    { 147, 15, 3 },
                    { 148, 15, 3 },
                    { 149, 15, 3 },
                    { 150, 15, 3 },
                    { 151, 16, 5 },
                    { 152, 16, 5 },
                    { 153, 16, 5 },
                    { 154, 16, 5 },
                    { 155, 16, 5 },
                    { 156, 16, 4 },
                    { 157, 16, 4 },
                    { 158, 16, 4 },
                    { 159, 16, 4 },
                    { 160, 16, 4 },
                    { 161, 17, 5 },
                    { 162, 17, 5 },
                    { 163, 17, 5 },
                    { 164, 17, 5 },
                    { 165, 17, 4 },
                    { 166, 17, 4 },
                    { 167, 17, 4 },
                    { 168, 17, 4 },
                    { 169, 17, 4 },
                    { 170, 17, 4 },
                    { 171, 18, 5 },
                    { 172, 18, 5 },
                    { 173, 18, 5 },
                    { 174, 18, 4 },
                    { 175, 18, 4 },
                    { 176, 18, 4 },
                    { 177, 18, 4 },
                    { 178, 18, 4 },
                    { 179, 18, 4 },
                    { 180, 18, 4 },
                    { 181, 19, 5 },
                    { 182, 19, 5 },
                    { 183, 19, 4 },
                    { 184, 19, 4 },
                    { 185, 19, 4 },
                    { 186, 19, 4 },
                    { 187, 19, 4 },
                    { 188, 19, 4 },
                    { 189, 19, 4 },
                    { 190, 19, 4 },
                    { 191, 20, 5 },
                    { 192, 20, 4 },
                    { 193, 20, 4 },
                    { 194, 20, 4 },
                    { 195, 20, 4 },
                    { 196, 20, 4 },
                    { 197, 20, 4 },
                    { 198, 20, 4 },
                    { 199, 20, 4 },
                    { 200, 20, 4 },
                    { 201, 21, 4 },
                    { 202, 21, 4 },
                    { 203, 21, 4 },
                    { 204, 21, 4 },
                    { 205, 21, 4 },
                    { 206, 21, 4 },
                    { 207, 21, 4 },
                    { 208, 21, 4 },
                    { 209, 21, 4 },
                    { 210, 21, 4 },
                    { 211, 22, 4 },
                    { 212, 22, 4 },
                    { 213, 22, 4 },
                    { 214, 22, 4 },
                    { 215, 22, 4 },
                    { 216, 22, 4 },
                    { 217, 22, 4 },
                    { 218, 22, 4 },
                    { 219, 22, 4 },
                    { 220, 22, 3 },
                    { 221, 23, 4 },
                    { 222, 23, 4 },
                    { 223, 23, 4 },
                    { 224, 23, 4 },
                    { 225, 23, 4 },
                    { 226, 23, 4 },
                    { 227, 23, 4 },
                    { 228, 23, 4 },
                    { 229, 23, 3 },
                    { 230, 23, 3 },
                    { 231, 24, 4 },
                    { 232, 24, 4 },
                    { 233, 24, 4 },
                    { 234, 24, 4 },
                    { 235, 24, 4 },
                    { 236, 24, 4 },
                    { 237, 24, 4 },
                    { 238, 24, 3 },
                    { 239, 24, 3 },
                    { 240, 24, 3 },
                    { 241, 25, 4 },
                    { 242, 25, 4 },
                    { 243, 25, 4 },
                    { 244, 25, 4 },
                    { 245, 25, 4 },
                    { 246, 25, 4 },
                    { 247, 25, 3 },
                    { 248, 25, 3 },
                    { 249, 25, 3 },
                    { 250, 25, 3 },
                    { 251, 26, 4 },
                    { 252, 26, 4 },
                    { 253, 26, 4 },
                    { 254, 26, 4 },
                    { 255, 26, 4 },
                    { 256, 26, 3 },
                    { 257, 26, 3 },
                    { 258, 26, 3 },
                    { 259, 26, 3 },
                    { 260, 26, 3 },
                    { 261, 27, 4 },
                    { 262, 27, 4 },
                    { 263, 27, 4 },
                    { 264, 27, 4 },
                    { 265, 27, 3 },
                    { 266, 27, 3 },
                    { 267, 27, 3 },
                    { 268, 27, 3 },
                    { 269, 27, 3 },
                    { 270, 27, 3 },
                    { 271, 28, 4 },
                    { 272, 28, 4 },
                    { 273, 28, 4 },
                    { 274, 28, 3 },
                    { 275, 28, 3 },
                    { 276, 28, 3 },
                    { 277, 28, 3 },
                    { 278, 28, 3 },
                    { 279, 28, 3 },
                    { 280, 28, 3 },
                    { 281, 29, 4 },
                    { 282, 29, 4 },
                    { 283, 29, 3 },
                    { 284, 29, 3 },
                    { 285, 29, 3 },
                    { 286, 29, 3 },
                    { 287, 29, 3 },
                    { 288, 29, 3 },
                    { 289, 29, 3 },
                    { 290, 29, 3 },
                    { 291, 30, 4 },
                    { 292, 30, 3 },
                    { 293, 30, 3 },
                    { 294, 30, 3 },
                    { 295, 30, 3 },
                    { 296, 30, 3 },
                    { 297, 30, 3 },
                    { 298, 30, 3 },
                    { 299, 30, 3 },
                    { 300, 30, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

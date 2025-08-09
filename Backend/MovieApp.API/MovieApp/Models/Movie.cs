
namespace MovieApp.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public MediaType Type { get; set; }
    public string CoverImage { get; set; }
    public string ShortDescription { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<string> Cast { get; set; }
    public List<Rating> Ratings { get; set; } = new();
    public Movie()
    {
        Cast = new List<string>();
    }
}

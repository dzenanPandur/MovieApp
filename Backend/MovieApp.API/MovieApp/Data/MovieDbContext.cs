using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public void SeedCastData()
        {
            if (Movies.Any() && !Movies.First().Cast.Any())
            {
                var castData = new Dictionary<int, List<string>>
                {
                    { 1, new List<string> { "Tim Robbins", "Morgan Freeman" } },
                    { 2, new List<string> { "Marlon Brando", "Al Pacino" } },
                    { 3, new List<string> { "Christian Bale", "Heath Ledger" } },
                    { 4, new List<string> { "Al Pacino", "Robert De Niro" } },
                    { 5, new List<string> { "Henry Fonda", "Lee J. Cobb" } },
                    { 6, new List<string> { "Liam Neeson", "Ralph Fiennes" } },
                    { 7, new List<string> { "Elijah Wood", "Viggo Mortensen" } },
                    { 8, new List<string> { "John Travolta", "Samuel L. Jackson" } },
                    { 9, new List<string> { "Elijah Wood", "Ian McKellen" } },
                    { 10, new List<string> { "Tom Hanks", "Robin Wright" } },
                    { 11, new List<string> { "Brad Pitt", "Edward Norton" } },
                    { 12, new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" } },
                    { 13, new List<string> { "Elijah Wood", "Sean Astin" } },
                    { 14, new List<string> { "Mark Hamill", "Harrison Ford" } },
                    { 15, new List<string> { "Keanu Reeves", "Laurence Fishburne" } },
                    { 16, new List<string> { "Bryan Cranston", "Aaron Paul" } },
                    { 17, new List<string> { "Emilia Clarke", "Kit Harington" } },
                    { 18, new List<string> { "Dominic West", "Lance Reddick" } },
                    { 19, new List<string> { "Millie Bobby Brown", "Finn Wolfhard" } },
                    { 20, new List<string> { "James Gandolfini", "Edie Falco" } },
                    { 21, new List<string> { "Benedict Cumberbatch", "Martin Freeman" } },
                    { 22, new List<string> { "Jennifer Aniston", "Courteney Cox" } },
                    { 23, new List<string> { "Steve Carell", "John Krasinski" } },
                    { 24, new List<string> { "Bob Odenkirk", "Rhea Seehorn" } },
                    { 25, new List<string> { "Kevin Spacey", "Robin Wright" } },
                    { 26, new List<string> { "Jared Harris", "Stellan Skarsgård" } },
                    { 27, new List<string> { "Pedro Pascal", "Gina Carano" } },
                    { 28, new List<string> { "Matthew Fox", "Evangeline Lilly" } },
                    { 29, new List<string> { "Matthew McConaughey", "Woody Harrelson" } },
                    { 30, new List<string> { "Jerry Seinfeld", "Julia Louis-Dreyfus" } }
                };

                foreach (var movie in Movies)
                {
                    if (castData.ContainsKey(movie.Id))
                    {
                        movie.Cast = castData[movie.Id];
                    }
                }
                SaveChanges();
            }
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .Property(m => m.Cast)
                .HasConversion(
                    v => string.Join(';', v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

           
            SeedUsers(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
    new Movie { Id = 1, Title = "The Shawshank Redemption", Type = MediaType.Movie, ShortDescription = "Two imprisoned men bond over a number of years.", ReleaseDate = new DateTime(1994, 9, 22), CoverImage = "shawshank.jpg" },
    new Movie { Id = 2, Title = "The Godfather", Type = MediaType.Movie, ShortDescription = "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.", ReleaseDate = new DateTime(1972, 3, 24), CoverImage = "godfather.jpg" },
    new Movie { Id = 3, Title = "The Dark Knight", Type = MediaType.Movie, ShortDescription = "Batman faces the Joker, a criminal mastermind.", ReleaseDate = new DateTime(2008, 7, 18), CoverImage = "darkknight.jpg" },
    new Movie { Id = 4, Title = "The Godfather Part II", Type = MediaType.Movie, ShortDescription = "The early life and career of Vito Corleone.", ReleaseDate = new DateTime(1974, 12, 20), CoverImage = "godfather2.jpg" },
    new Movie { Id = 5, Title = "12 Angry Men", Type = MediaType.Movie, ShortDescription = "A jury holdout attempts to prevent a miscarriage of justice.", ReleaseDate = new DateTime(1957, 4, 10), CoverImage = "12angrymen.jpg" },
    new Movie { Id = 6, Title = "Schindler's List", Type = MediaType.Movie, ShortDescription = "In German-occupied Poland during WWII, Oskar Schindler becomes concerned for his Jewish workforce.", ReleaseDate = new DateTime(1993, 12, 15), CoverImage = "schindler.jpg" },
    new Movie { Id = 7, Title = "The Lord of the Rings: The Return of the King", Type = MediaType.Movie, ShortDescription = "Gandalf and Aragorn lead the World of Men against Sauron's army.", ReleaseDate = new DateTime(2003, 12, 17), CoverImage = "lotr3.jpg" },
    new Movie { Id = 8, Title = "Pulp Fiction", Type = MediaType.Movie, ShortDescription = "The lives of two mob hitmen, a boxer, and others intertwine.", ReleaseDate = new DateTime(1994, 10, 14), CoverImage = "pulpfiction.jpg" },
    new Movie { Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Type = MediaType.Movie, ShortDescription = "A meek Hobbit sets out on a journey to destroy the One Ring.", ReleaseDate = new DateTime(2001, 12, 19), CoverImage = "lotr1.jpg" },
    new Movie { Id = 10, Title = "Forrest Gump", Type = MediaType.Movie, ShortDescription = "The presidencies of Kennedy and Johnson, the Vietnam War, and more through the eyes of Forrest Gump.", ReleaseDate = new DateTime(1994, 7, 6), CoverImage = "forrestgump.jpg" },
    new Movie { Id = 11, Title = "Fight Club", Type = MediaType.Movie, ShortDescription = "An insomniac office worker forms an underground fight club.", ReleaseDate = new DateTime(1999, 10, 15), CoverImage = "fightclub.jpg" },
    new Movie { Id = 12, Title = "Inception", Type = MediaType.Movie, ShortDescription = "A thief who steals corporate secrets through dream-sharing technology.", ReleaseDate = new DateTime(2010, 7, 16), CoverImage = "inception.jpg" },
    new Movie { Id = 13, Title = "The Lord of the Rings: The Two Towers", Type = MediaType.Movie, ShortDescription = "While Frodo and Sam edge closer to Mordor, the divided fellowship makes a stand.", ReleaseDate = new DateTime(2002, 12, 18), CoverImage = "lotr2.jpg" },
    new Movie { Id = 14, Title = "Star Wars: Episode V - The Empire Strikes Back", Type = MediaType.Movie, ShortDescription = "After the rebels are brutally overpowered, Luke begins Jedi training.", ReleaseDate = new DateTime(1980, 5, 21), CoverImage = "empirestrikesback.jpg" },
    new Movie { Id = 15, Title = "The Matrix", Type = MediaType.Movie, ShortDescription = "A computer hacker learns about the true nature of reality.", ReleaseDate = new DateTime(1999, 3, 31), CoverImage = "matrix.jpg" },

    
    new Movie { Id = 16, Title = "Breaking Bad", Type = MediaType.TvShow, ShortDescription = "A high school chemistry teacher turned methamphetamine producer.", ReleaseDate = new DateTime(2008, 1, 20), CoverImage = "breakingbad.jpg" },
    new Movie { Id = 17, Title = "Game of Thrones", Type = MediaType.TvShow, ShortDescription = "Nine noble families wage war against each other in Westeros.", ReleaseDate = new DateTime(2011, 4, 17), CoverImage = "got.jpg" },
    new Movie { Id = 18, Title = "The Wire", Type = MediaType.TvShow, ShortDescription = "Baltimore drug scene, seen through the eyes of law enforcement and drug dealers.", ReleaseDate = new DateTime(2002, 6, 2), CoverImage = "thewire.jpg" },
    new Movie { Id = 19, Title = "Stranger Things", Type = MediaType.TvShow, ShortDescription = "A group of young friends witness supernatural forces and secret government exploits.", ReleaseDate = new DateTime(2016, 7, 15), CoverImage = "strangerthings.jpg" },
    new Movie { Id = 20, Title = "The Sopranos", Type = MediaType.TvShow, ShortDescription = "New Jersey mob boss Tony Soprano deals with personal and professional issues.", ReleaseDate = new DateTime(1999, 1, 10), CoverImage = "sopranos.jpg" },
    new Movie { Id = 21, Title = "Sherlock", Type = MediaType.TvShow, ShortDescription = "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.", ReleaseDate = new DateTime(2010, 7, 25), CoverImage = "sherlock.jpg" },
    new Movie { Id = 22, Title = "Friends", Type = MediaType.TvShow, ShortDescription = "Follows the personal and professional lives of six twenty to thirty-something friends.", ReleaseDate = new DateTime(1994, 9, 22), CoverImage = "friends.jpg" },
    new Movie { Id = 23, Title = "The Office", Type = MediaType.TvShow, ShortDescription = "A mockumentary on a group of typical office workers.", ReleaseDate = new DateTime(2005, 3, 24), CoverImage = "theoffice.jpg" },
    new Movie { Id = 24, Title = "Better Call Saul", Type = MediaType.TvShow, ShortDescription = "The trials and tribulations of criminal lawyer Jimmy McGill.", ReleaseDate = new DateTime(2015, 2, 8), CoverImage = "bettercallsaul.jpg" },
    new Movie { Id = 25, Title = "House of Cards", Type = MediaType.TvShow, ShortDescription = "A Congressman works with his equally conniving wife to exact revenge.", ReleaseDate = new DateTime(2013, 2, 1), CoverImage = "houseofcards.jpg" },
    new Movie { Id = 26, Title = "Chernobyl", Type = MediaType.TvShow, ShortDescription = "A dramatization of the true story of the Chernobyl nuclear disaster.", ReleaseDate = new DateTime(2019, 5, 6), CoverImage = "chernobyl.jpg" },
    new Movie { Id = 27, Title = "The Mandalorian", Type = MediaType.TvShow, ShortDescription = "A lone gunfighter makes his way through the outer reaches of the galaxy.", ReleaseDate = new DateTime(2019, 11, 12), CoverImage = "mandalorian.jpg" },
    new Movie { Id = 28, Title = "Lost", Type = MediaType.TvShow, ShortDescription = "The survivors of a plane crash are forced to work together.", ReleaseDate = new DateTime(2004, 9, 22), CoverImage = "lost.jpg" },
    new Movie { Id = 29, Title = "True Detective", Type = MediaType.TvShow, ShortDescription = "Seasonal anthology series in which police investigations unearth personal and professional secrets.", ReleaseDate = new DateTime(2014, 1, 12), CoverImage = "truedetective.jpg" },
    new Movie { Id = 30, Title = "Seinfeld", Type = MediaType.TvShow, ShortDescription = "The continuing misadventures of neurotic New York stand-up comedian Jerry Seinfeld.", ReleaseDate = new DateTime(1989, 7, 5), CoverImage = "seinfeld.jpg" }
);

            modelBuilder.Entity<Rating>().HasData(
    new Rating { Id = 1, Value = 5, MovieId = 1 },
    new Rating { Id = 2, Value = 5, MovieId = 1 },
    new Rating { Id = 3, Value = 5, MovieId = 1 },
    new Rating { Id = 4, Value = 5, MovieId = 1 },
    new Rating { Id = 5, Value = 5, MovieId = 1 },
    new Rating { Id = 6, Value = 5, MovieId = 1 },
    new Rating { Id = 7, Value = 5, MovieId = 1 },
    new Rating { Id = 8, Value = 5, MovieId = 1 },
    new Rating { Id = 9, Value = 5, MovieId = 1 },
    new Rating { Id = 10, Value = 4, MovieId = 1 },


    new Rating { Id = 11, Value = 5, MovieId = 2 },
    new Rating { Id = 12, Value = 5, MovieId = 2 },
    new Rating { Id = 13, Value = 5, MovieId = 2 },
    new Rating { Id = 14, Value = 5, MovieId = 2 },
    new Rating { Id = 15, Value = 5, MovieId = 2 },
    new Rating { Id = 16, Value = 5, MovieId = 2 },
    new Rating { Id = 17, Value = 5, MovieId = 2 },
    new Rating { Id = 18, Value = 5, MovieId = 2 },
    new Rating { Id = 19, Value = 4, MovieId = 2 },
    new Rating { Id = 20, Value = 4, MovieId = 2 },


    new Rating { Id = 21, Value = 5, MovieId = 3 },
    new Rating { Id = 22, Value = 5, MovieId = 3 },
    new Rating { Id = 23, Value = 5, MovieId = 3 },
    new Rating { Id = 24, Value = 5, MovieId = 3 },
    new Rating { Id = 25, Value = 5, MovieId = 3 },
    new Rating { Id = 26, Value = 5, MovieId = 3 },
    new Rating { Id = 27, Value = 5, MovieId = 3 },
    new Rating { Id = 28, Value = 4, MovieId = 3 },
    new Rating { Id = 29, Value = 4, MovieId = 3 },
    new Rating { Id = 30, Value = 4, MovieId = 3 },

    new Rating { Id = 31, Value = 5, MovieId = 4 },
    new Rating { Id = 32, Value = 5, MovieId = 4 },
    new Rating { Id = 33, Value = 5, MovieId = 4 },
    new Rating { Id = 34, Value = 5, MovieId = 4 },
    new Rating { Id = 35, Value = 5, MovieId = 4 },
    new Rating { Id = 36, Value = 5, MovieId = 4 },
    new Rating { Id = 37, Value = 4, MovieId = 4 },
    new Rating { Id = 38, Value = 4, MovieId = 4 },
    new Rating { Id = 39, Value = 4, MovieId = 4 },
    new Rating { Id = 40, Value = 4, MovieId = 4 },


    new Rating { Id = 41, Value = 5, MovieId = 5 },
    new Rating { Id = 42, Value = 5, MovieId = 5 },
    new Rating { Id = 43, Value = 5, MovieId = 5 },
    new Rating { Id = 44, Value = 5, MovieId = 5 },
    new Rating { Id = 45, Value = 5, MovieId = 5 },
    new Rating { Id = 46, Value = 4, MovieId = 5 },
    new Rating { Id = 47, Value = 4, MovieId = 5 },
    new Rating { Id = 48, Value = 4, MovieId = 5 },
    new Rating { Id = 49, Value = 4, MovieId = 5 },
    new Rating { Id = 50, Value = 4, MovieId = 5 },

    new Rating { Id = 51, Value = 5, MovieId = 6 },
    new Rating { Id = 52, Value = 5, MovieId = 6 },
    new Rating { Id = 53, Value = 5, MovieId = 6 },
    new Rating { Id = 54, Value = 5, MovieId = 6 },
    new Rating { Id = 55, Value = 4, MovieId = 6 },
    new Rating { Id = 56, Value = 4, MovieId = 6 },
    new Rating { Id = 57, Value = 4, MovieId = 6 },
    new Rating { Id = 58, Value = 4, MovieId = 6 },
    new Rating { Id = 59, Value = 4, MovieId = 6 },
    new Rating { Id = 60, Value = 4, MovieId = 6 },

    new Rating { Id = 61, Value = 5, MovieId = 7 },
    new Rating { Id = 62, Value = 5, MovieId = 7 },
    new Rating { Id = 63, Value = 5, MovieId = 7 },
    new Rating { Id = 64, Value = 4, MovieId = 7 },
    new Rating { Id = 65, Value = 4, MovieId = 7 },
    new Rating { Id = 66, Value = 4, MovieId = 7 },
    new Rating { Id = 67, Value = 4, MovieId = 7 },
    new Rating { Id = 68, Value = 4, MovieId = 7 },
    new Rating { Id = 69, Value = 4, MovieId = 7 },
    new Rating { Id = 70, Value = 4, MovieId = 7 },


    new Rating { Id = 71, Value = 5, MovieId = 8 },
    new Rating { Id = 72, Value = 5, MovieId = 8 },
    new Rating { Id = 73, Value = 4, MovieId = 8 },
    new Rating { Id = 74, Value = 4, MovieId = 8 },
    new Rating { Id = 75, Value = 4, MovieId = 8 },
    new Rating { Id = 76, Value = 4, MovieId = 8 },
    new Rating { Id = 77, Value = 4, MovieId = 8 },
    new Rating { Id = 78, Value = 4, MovieId = 8 },
    new Rating { Id = 79, Value = 4, MovieId = 8 },
    new Rating { Id = 80, Value = 4, MovieId = 8 },


    new Rating { Id = 81, Value = 5, MovieId = 9 },
    new Rating { Id = 82, Value = 4, MovieId = 9 },
    new Rating { Id = 83, Value = 4, MovieId = 9 },
    new Rating { Id = 84, Value = 4, MovieId = 9 },
    new Rating { Id = 85, Value = 4, MovieId = 9 },
    new Rating { Id = 86, Value = 4, MovieId = 9 },
    new Rating { Id = 87, Value = 4, MovieId = 9 },
    new Rating { Id = 88, Value = 4, MovieId = 9 },
    new Rating { Id = 89, Value = 4, MovieId = 9 },
    new Rating { Id = 90, Value = 4, MovieId = 9 },


    new Rating { Id = 91, Value = 4, MovieId = 10 },
    new Rating { Id = 92, Value = 4, MovieId = 10 },
    new Rating { Id = 93, Value = 4, MovieId = 10 },
    new Rating { Id = 94, Value = 4, MovieId = 10 },
    new Rating { Id = 95, Value = 4, MovieId = 10 },
    new Rating { Id = 96, Value = 4, MovieId = 10 },
    new Rating { Id = 97, Value = 4, MovieId = 10 },
    new Rating { Id = 98, Value = 4, MovieId = 10 },
    new Rating { Id = 99, Value = 4, MovieId = 10 },
    new Rating { Id = 100, Value = 4, MovieId = 10 },

    new Rating { Id = 101, Value = 4, MovieId = 11 },
    new Rating { Id = 102, Value = 4, MovieId = 11 },
    new Rating { Id = 103, Value = 4, MovieId = 11 },
    new Rating { Id = 104, Value = 4, MovieId = 11 },
    new Rating { Id = 105, Value = 4, MovieId = 11 },
    new Rating { Id = 106, Value = 4, MovieId = 11 },
    new Rating { Id = 107, Value = 4, MovieId = 11 },
    new Rating { Id = 108, Value = 4, MovieId = 11 },
    new Rating { Id = 109, Value = 4, MovieId = 11 },
    new Rating { Id = 110, Value = 3, MovieId = 11 },

    new Rating { Id = 111, Value = 4, MovieId = 12 },
    new Rating { Id = 112, Value = 4, MovieId = 12 },
    new Rating { Id = 113, Value = 4, MovieId = 12 },
    new Rating { Id = 114, Value = 4, MovieId = 12 },
    new Rating { Id = 115, Value = 4, MovieId = 12 },
    new Rating { Id = 116, Value = 4, MovieId = 12 },
    new Rating { Id = 117, Value = 4, MovieId = 12 },
    new Rating { Id = 118, Value = 4, MovieId = 12 },
    new Rating { Id = 119, Value = 3, MovieId = 12 },
    new Rating { Id = 120, Value = 3, MovieId = 12 },


    new Rating { Id = 121, Value = 4, MovieId = 13 },
    new Rating { Id = 122, Value = 4, MovieId = 13 },
    new Rating { Id = 123, Value = 4, MovieId = 13 },
    new Rating { Id = 124, Value = 4, MovieId = 13 },
    new Rating { Id = 125, Value = 4, MovieId = 13 },
    new Rating { Id = 126, Value = 4, MovieId = 13 },
    new Rating { Id = 127, Value = 4, MovieId = 13 },
    new Rating { Id = 128, Value = 3, MovieId = 13 },
    new Rating { Id = 129, Value = 3, MovieId = 13 },
    new Rating { Id = 130, Value = 3, MovieId = 13 },


    new Rating { Id = 131, Value = 4, MovieId = 14 },
    new Rating { Id = 132, Value = 4, MovieId = 14 },
    new Rating { Id = 133, Value = 4, MovieId = 14 },
    new Rating { Id = 134, Value = 4, MovieId = 14 },
    new Rating { Id = 135, Value = 4, MovieId = 14 },
    new Rating { Id = 136, Value = 4, MovieId = 14 },
    new Rating { Id = 137, Value = 3, MovieId = 14 },
    new Rating { Id = 138, Value = 3, MovieId = 14 },
    new Rating { Id = 139, Value = 3, MovieId = 14 },
    new Rating { Id = 140, Value = 3, MovieId = 14 },


    new Rating { Id = 141, Value = 4, MovieId = 15 },
    new Rating { Id = 142, Value = 4, MovieId = 15 },
    new Rating { Id = 143, Value = 4, MovieId = 15 },
    new Rating { Id = 144, Value = 4, MovieId = 15 },
    new Rating { Id = 145, Value = 4, MovieId = 15 },
    new Rating { Id = 146, Value = 3, MovieId = 15 },
    new Rating { Id = 147, Value = 3, MovieId = 15 },
    new Rating { Id = 148, Value = 3, MovieId = 15 },
    new Rating { Id = 149, Value = 3, MovieId = 15 },
    new Rating { Id = 150, Value = 3, MovieId = 15 },


    new Rating { Id = 151, Value = 5, MovieId = 16 },
    new Rating { Id = 152, Value = 5, MovieId = 16 },
    new Rating { Id = 153, Value = 5, MovieId = 16 },
    new Rating { Id = 154, Value = 5, MovieId = 16 },
    new Rating { Id = 155, Value = 5, MovieId = 16 },
    new Rating { Id = 156, Value = 4, MovieId = 16 },
    new Rating { Id = 157, Value = 4, MovieId = 16 },
    new Rating { Id = 158, Value = 4, MovieId = 16 },
    new Rating { Id = 159, Value = 4, MovieId = 16 },
    new Rating { Id = 160, Value = 4, MovieId = 16 },

    new Rating { Id = 161, Value = 5, MovieId = 17 },
    new Rating { Id = 162, Value = 5, MovieId = 17 },
    new Rating { Id = 163, Value = 5, MovieId = 17 },
    new Rating { Id = 164, Value = 5, MovieId = 17 },
    new Rating { Id = 165, Value = 4, MovieId = 17 },
    new Rating { Id = 166, Value = 4, MovieId = 17 },
    new Rating { Id = 167, Value = 4, MovieId = 17 },
    new Rating { Id = 168, Value = 4, MovieId = 17 },
    new Rating { Id = 169, Value = 4, MovieId = 17 },
    new Rating { Id = 170, Value = 4, MovieId = 17 },

    new Rating { Id = 171, Value = 5, MovieId = 18 },
    new Rating { Id = 172, Value = 5, MovieId = 18 },
    new Rating { Id = 173, Value = 5, MovieId = 18 },
    new Rating { Id = 174, Value = 4, MovieId = 18 },
    new Rating { Id = 175, Value = 4, MovieId = 18 },
    new Rating { Id = 176, Value = 4, MovieId = 18 },
    new Rating { Id = 177, Value = 4, MovieId = 18 },
    new Rating { Id = 178, Value = 4, MovieId = 18 },
    new Rating { Id = 179, Value = 4, MovieId = 18 },
    new Rating { Id = 180, Value = 4, MovieId = 18 },

    new Rating { Id = 181, Value = 5, MovieId = 19 },
    new Rating { Id = 182, Value = 5, MovieId = 19 },
    new Rating { Id = 183, Value = 4, MovieId = 19 },
    new Rating { Id = 184, Value = 4, MovieId = 19 },
    new Rating { Id = 185, Value = 4, MovieId = 19 },
    new Rating { Id = 186, Value = 4, MovieId = 19 },
    new Rating { Id = 187, Value = 4, MovieId = 19 },
    new Rating { Id = 188, Value = 4, MovieId = 19 },
    new Rating { Id = 189, Value = 4, MovieId = 19 },
    new Rating { Id = 190, Value = 4, MovieId = 19 },

    new Rating { Id = 191, Value = 5, MovieId = 20 },
    new Rating { Id = 192, Value = 4, MovieId = 20 },
    new Rating { Id = 193, Value = 4, MovieId = 20 },
    new Rating { Id = 194, Value = 4, MovieId = 20 },
    new Rating { Id = 195, Value = 4, MovieId = 20 },
    new Rating { Id = 196, Value = 4, MovieId = 20 },
    new Rating { Id = 197, Value = 4, MovieId = 20 },
    new Rating { Id = 198, Value = 4, MovieId = 20 },
    new Rating { Id = 199, Value = 4, MovieId = 20 },
    new Rating { Id = 200, Value = 4, MovieId = 20 },

    new Rating { Id = 201, Value = 4, MovieId = 21 },
    new Rating { Id = 202, Value = 4, MovieId = 21 },
    new Rating { Id = 203, Value = 4, MovieId = 21 },
    new Rating { Id = 204, Value = 4, MovieId = 21 },
    new Rating { Id = 205, Value = 4, MovieId = 21 },
    new Rating { Id = 206, Value = 4, MovieId = 21 },
    new Rating { Id = 207, Value = 4, MovieId = 21 },
    new Rating { Id = 208, Value = 4, MovieId = 21 },
    new Rating { Id = 209, Value = 4, MovieId = 21 },
    new Rating { Id = 210, Value = 4, MovieId = 21 },

    new Rating { Id = 211, Value = 4, MovieId = 22 },
    new Rating { Id = 212, Value = 4, MovieId = 22 },
    new Rating { Id = 213, Value = 4, MovieId = 22 },
    new Rating { Id = 214, Value = 4, MovieId = 22 },
    new Rating { Id = 215, Value = 4, MovieId = 22 },
    new Rating { Id = 216, Value = 4, MovieId = 22 },
    new Rating { Id = 217, Value = 4, MovieId = 22 },
    new Rating { Id = 218, Value = 4, MovieId = 22 },
    new Rating { Id = 219, Value = 4, MovieId = 22 },
    new Rating { Id = 220, Value = 3, MovieId = 22 },

    new Rating { Id = 221, Value = 4, MovieId = 23 },
    new Rating { Id = 222, Value = 4, MovieId = 23 },
    new Rating { Id = 223, Value = 4, MovieId = 23 },
    new Rating { Id = 224, Value = 4, MovieId = 23 },
    new Rating { Id = 225, Value = 4, MovieId = 23 },
    new Rating { Id = 226, Value = 4, MovieId = 23 },
    new Rating { Id = 227, Value = 4, MovieId = 23 },
    new Rating { Id = 228, Value = 4, MovieId = 23 },
    new Rating { Id = 229, Value = 3, MovieId = 23 },
    new Rating { Id = 230, Value = 3, MovieId = 23 },

    new Rating { Id = 231, Value = 4, MovieId = 24 },
    new Rating { Id = 232, Value = 4, MovieId = 24 },
    new Rating { Id = 233, Value = 4, MovieId = 24 },
    new Rating { Id = 234, Value = 4, MovieId = 24 },
    new Rating { Id = 235, Value = 4, MovieId = 24 },
    new Rating { Id = 236, Value = 4, MovieId = 24 },
    new Rating { Id = 237, Value = 4, MovieId = 24 },
    new Rating { Id = 238, Value = 3, MovieId = 24 },
    new Rating { Id = 239, Value = 3, MovieId = 24 },
    new Rating { Id = 240, Value = 3, MovieId = 24 },

    new Rating { Id = 241, Value = 4, MovieId = 25 },
    new Rating { Id = 242, Value = 4, MovieId = 25 },
    new Rating { Id = 243, Value = 4, MovieId = 25 },
    new Rating { Id = 244, Value = 4, MovieId = 25 },
    new Rating { Id = 245, Value = 4, MovieId = 25 },
    new Rating { Id = 246, Value = 4, MovieId = 25 },
    new Rating { Id = 247, Value = 3, MovieId = 25 },
    new Rating { Id = 248, Value = 3, MovieId = 25 },
    new Rating { Id = 249, Value = 3, MovieId = 25 },
    new Rating { Id = 250, Value = 3, MovieId = 25 },

    new Rating { Id = 251, Value = 4, MovieId = 26 },
    new Rating { Id = 252, Value = 4, MovieId = 26 },
    new Rating { Id = 253, Value = 4, MovieId = 26 },
    new Rating { Id = 254, Value = 4, MovieId = 26 },
    new Rating { Id = 255, Value = 4, MovieId = 26 },
    new Rating { Id = 256, Value = 3, MovieId = 26 },
    new Rating { Id = 257, Value = 3, MovieId = 26 },
    new Rating { Id = 258, Value = 3, MovieId = 26 },
    new Rating { Id = 259, Value = 3, MovieId = 26 },
    new Rating { Id = 260, Value = 3, MovieId = 26 },

    new Rating { Id = 261, Value = 4, MovieId = 27 },
    new Rating { Id = 262, Value = 4, MovieId = 27 },
    new Rating { Id = 263, Value = 4, MovieId = 27 },
    new Rating { Id = 264, Value = 4, MovieId = 27 },
    new Rating { Id = 265, Value = 3, MovieId = 27 },
    new Rating { Id = 266, Value = 3, MovieId = 27 },
    new Rating { Id = 267, Value = 3, MovieId = 27 },
    new Rating { Id = 268, Value = 3, MovieId = 27 },
    new Rating { Id = 269, Value = 3, MovieId = 27 },
    new Rating { Id = 270, Value = 3, MovieId = 27 },

    new Rating { Id = 271, Value = 4, MovieId = 28 },
    new Rating { Id = 272, Value = 4, MovieId = 28 },
    new Rating { Id = 273, Value = 4, MovieId = 28 },
    new Rating { Id = 274, Value = 3, MovieId = 28 },
    new Rating { Id = 275, Value = 3, MovieId = 28 },
    new Rating { Id = 276, Value = 3, MovieId = 28 },
    new Rating { Id = 277, Value = 3, MovieId = 28 },
    new Rating { Id = 278, Value = 3, MovieId = 28 },
    new Rating { Id = 279, Value = 3, MovieId = 28 },
    new Rating { Id = 280, Value = 3, MovieId = 28 },

    new Rating { Id = 281, Value = 4, MovieId = 29 },
    new Rating { Id = 282, Value = 4, MovieId = 29 },
    new Rating { Id = 283, Value = 3, MovieId = 29 },
    new Rating { Id = 284, Value = 3, MovieId = 29 },
    new Rating { Id = 285, Value = 3, MovieId = 29 },
    new Rating { Id = 286, Value = 3, MovieId = 29 },
    new Rating { Id = 287, Value = 3, MovieId = 29 },
    new Rating { Id = 288, Value = 3, MovieId = 29 },
    new Rating { Id = 289, Value = 3, MovieId = 29 },
    new Rating { Id = 290, Value = 3, MovieId = 29 },

    new Rating { Id = 291, Value = 4, MovieId = 30 },
    new Rating { Id = 292, Value = 3, MovieId = 30 },
    new Rating { Id = 293, Value = 3, MovieId = 30 },
    new Rating { Id = 294, Value = 3, MovieId = 30 },
    new Rating { Id = 295, Value = 3, MovieId = 30 },
    new Rating { Id = 296, Value = 3, MovieId = 30 },
    new Rating { Id = 297, Value = 3, MovieId = 30 },
    new Rating { Id = 298, Value = 3, MovieId = 30 },
    new Rating { Id = 299, Value = 3, MovieId = 30 },
    new Rating { Id = 300, Value = 3, MovieId = 30 }
);
        }
           
        private void SeedUsers(ModelBuilder builder)
        {

            var adminUser = new ApplicationUser
            {
                Id = "BCAC500D-AFF6-47D2-BB76-6430FCC1FE83",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@movieapp.com",
                NormalizedEmail = "ADMIN@MOVIEAPP.COM",
                EmailConfirmed = true,
                SecurityStamp = "2bb56497-5c49-4c01-8bc7-e2df21ef5d53",
                ConcurrencyStamp = "d1f1aabf-39e5-4ae4-9a4e-7a2676e9e9cd", 
                PasswordHash = "AQAAAAIAAYagAAAAEGQ74H878UZXZ2qrO3PGUCmbDkeR0pVC/YQ0BJQHFv50ks5DsM3WDpIZiB85F9hpRg=="
            };


            var regularUser = new ApplicationUser
            {
                Id = "E831C9E1-A746-4326-90FF-80BE4D20790D",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@movieapp.com",
                NormalizedEmail = "USER@MOVIEAPP.COM",
                EmailConfirmed = true,
                SecurityStamp = "76cbb512-37af-4f7a-aef4-4b52eb2c7618",
                ConcurrencyStamp = "f9b4e15c-1a5f-4c2e-812f-08a6c6135d5a",
                PasswordHash = "AQAAAAIAAYagAAAAEGQ74H878UZXZ2qrO3PGUCmbDkeR0pVC/YQ0BJQHFv50ks5DsM3WDpIZiB85F9hpRg=="
            };

            builder.Entity<ApplicationUser>().HasData(adminUser, regularUser);
        }
    }
}
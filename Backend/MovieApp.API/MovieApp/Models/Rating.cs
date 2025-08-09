using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieApp.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; }
    }
}
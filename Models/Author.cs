using System.Text.Json.Serialization;

namespace BookAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual List<Book>? Books { get; }
    }
}

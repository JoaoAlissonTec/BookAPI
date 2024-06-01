using Newtonsoft;
namespace BookAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Publish { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author? Author { get; }
    }
}

namespace BookAPI.Models
{
    public class PaginationResult
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}

using BookAPI.Models;

namespace BookAPI.Utils
{
    public class Util
    {
        public static PaginationResult CalculatePagination(int totalItems, int itemsPerPage, int currentPage)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, totalItems);

            return new PaginationResult
            {
                TotalPages = totalPages,
                CurrentPage = currentPage,
                StartIndex = startIndex,
                EndIndex = endIndex
            };
        }
    }
}

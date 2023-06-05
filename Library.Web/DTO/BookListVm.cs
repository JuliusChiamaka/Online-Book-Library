using Library.Domain.Entities;

namespace Library.Web.DTO
{
    public class BookListVm
    {
        public IQueryable<Book> booklist { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}

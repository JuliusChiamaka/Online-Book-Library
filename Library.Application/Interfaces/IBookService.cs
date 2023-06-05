using Library.Domain.Entities;
using Library.Shared.Model;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task AddBook(BookCreateDto book);
        Task AddCategory(CategoryCreationDto category);
        Task AddRatingForBook(RatingCreationDto rating);
        Task AddReviewForBook(ReviewCreationDto review);
        Task AddSubategoryForCategory(SubCategoryCreationDto subCategory);
        Task DeleteBook(Guid bookId);
        Task<IEnumerable<Book>> Search(string? isbn, string? title, string? author, string? publisher, string? yearPublished);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<SubCategory>> GetAllSubCategoryForCategory(Guid categoryId);
        Task<double> GetAverageRating(Guid bookId);
        Task<Book> GetSingleBook(Guid bookId);
        Task<IEnumerable<Review>> GetSingleBookReview(Guid bookId);
        Task UpdateBook(BookUpdateDto book);
    }
}
using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Shared.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IGenericRepository<Rating> _ratingRepo;
        private readonly IGenericRepository<Review> _reviewRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<SubCategory> _subCategoryRepo;
        private readonly IMapper _mapper;
       

        public BookService(IGenericRepository<Book> bookRepo, IMapper mapper, IGenericRepository<Rating> ratingRepo, IGenericRepository<Review> reviewRepo, IGenericRepository<SubCategory> subCategoryRepo)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _ratingRepo = ratingRepo;
            _reviewRepo = reviewRepo;
            _subCategoryRepo = subCategoryRepo;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _bookRepo.GetAll();
            return books;
        }

        // Books
        public async Task<Book> GetSingleBook(Guid bookId)
        {
            var book = await _bookRepo.GetById(bookId);
            return book;
        }

        public async Task AddBook(BookCreateDto book)
        {
            var _book = _mapper.Map<Book>(book);
            await _bookRepo.Add(_book);
        }

        public async Task UpdateBook(BookUpdateDto book)
        {
            var _book = _mapper.Map<Book>(book);
            await _bookRepo.Update(_book);
        }

        public async Task DeleteBook(Guid bookId)
        {
            var book = await _bookRepo.GetById(bookId);
            await _bookRepo.Remove(book);
        }

        public async Task<IEnumerable<Book>> Search(string? isbn, string? title, string? author, string? publisher, string? yearPublished)
        {
            Expression<Func<Book, bool>> expression = book =>
                (string.IsNullOrEmpty(isbn) || book.ISBN == isbn)
                || (string.IsNullOrEmpty(title) || book.Title.Contains(title))
                || (string.IsNullOrEmpty(author) || book.Author.Contains(author))
                || (string.IsNullOrEmpty(publisher) || book.Publisher.Contains(publisher)) ||
                (yearPublished == null || book.YearPublished.Year.ToString().Contains(yearPublished));

            var books = await _bookRepo.FindAll(expression);
            return books;
        }


        // Rating
        public async Task<double> GetAverageRating(Guid bookId)
        {
            Expression<Func<Rating, bool>> expression = rating => rating.BookId == bookId;
            var ratings = await _ratingRepo.FindAll(expression);
            double averageRating = ratings.Average(r => Convert.ToDouble(r.Rate));
            return averageRating;
        }

        public async Task AddRatingForBook(RatingCreationDto rating)
        {
            var _rating = _mapper.Map<Rating>(rating);
            await _ratingRepo.Add(_rating);
        }

        // Reviews
        public async Task<IEnumerable<Review>> GetSingleBookReview(Guid bookId)
        {
            Expression<Func<Review, bool>> expression = review => review.BookId == bookId;
            var reviews = await _reviewRepo.FindAll(expression);
            return reviews;

        }

        public async Task AddReviewForBook(ReviewCreationDto review)
        {
            var _review = _mapper.Map<Review>(review);
            await _reviewRepo.Add(_review);
        }

        // Categories
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAll();
            return categories;
        }

        public async Task AddCategory(CategoryCreationDto category)
        {
            var _category = _mapper.Map<Category>(category);
            await _categoryRepo.Add(_category);
        }

        // SubCategories
        public async Task<IEnumerable<SubCategory>> GetAllSubCategoryForCategory(Guid categoryId)
        {
            Expression<Func<SubCategory, bool>> expression = subCategory => subCategory.CategoryId == categoryId;

            var subCategories = await _subCategoryRepo.FindAll(expression);
            return subCategories;
        }

        public async Task AddSubategoryForCategory(SubCategoryCreationDto subCategory)
        {
            var _subCategory = _mapper.Map<SubCategory>(subCategory);
            await _subCategoryRepo.Add(_subCategory);
        }
    }
}

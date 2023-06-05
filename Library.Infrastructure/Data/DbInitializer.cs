using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder) 
        {
            var cat1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Fiction"
            };

            var cat2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Non-fiction"
            };



            var sub = new List<SubCategory> {
                new SubCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "History",
                    CategoryId = cat1.Id
                },
                new SubCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Biography",
                    CategoryId = cat2.Id
                }
            };

            var book1 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Publisher = "Scribner",
                ISBN = "978-0-7432-7356-5",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/91Mk2C3pvRL.jpg",
                YearPublished = new DateTime(1925, 4, 10),
                SubCategoryId= sub[0].Id,
            };
            var book2 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Publisher = "J.B. Lippincott & Co.",
                ISBN = "978-0-446-31078-0",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/91c5jn5ZSOL.jpg",
                YearPublished = new DateTime(1960, 7, 11),
                SubCategoryId = sub[1].Id,
            };

            var rat1 = new Rating
            {
                Id = Guid.NewGuid(),
                Rate = "5", 
                BookId = book1.Id
            };
            var rat2 = new Rating
            {
                Id = Guid.NewGuid(),
                Rate = "4",
                BookId = book2.Id
            };
            var rat3 = new Rating
            {
                Id = Guid.NewGuid(),
                Rate = "3",
                BookId = book1.Id
            };
            var rat4 = new Rating
            {
                Id = Guid.NewGuid(),
                Rate = "5",
                BookId = book2.Id
            };

            var rev1 = new Review
            {
                Id = Guid.NewGuid(),
                Message = "I absolutely loved this book! Highly recommend it to anyone.",
                BookId = book1.Id
            };
            var rev2 = new Review
            {
                Id = Guid.NewGuid(),
                Message = "Not really my cup of tea, but I can see why some people might enjoy it.",
                BookId = book1.Id
            };

            var rev3 = new Review
            {
                Id = Guid.NewGuid(),
                Message = "The writing was good, but I found the plot a bit predictable.",
                BookId = book2.Id
            };
            var rev4 = new Review
            {
                Id = Guid.NewGuid(),
                Message = "This book was a rollercoaster ride of emotions. Definitely worth a read.",
                BookId = book2.Id
            };

            modelBuilder.Entity<Category>().HasData( cat1, cat2);

            modelBuilder.Entity<Book>().HasData(book1, book2);
            modelBuilder.Entity<Rating>().HasData(rat1, rat2);
            modelBuilder.Entity<Review>().HasData(rev1, rev2);
            modelBuilder.Entity<SubCategory>().HasData(sub[0], sub[1]);

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        
        public Guid Id { get; set; } 

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime YearPublished { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Rating> Ratings { get; set; }

        public Guid SubCategoryId { get; set; }



        public Book()
        {
            Reviews = new List<Review>();
            Ratings = new List<Rating>();
        }

    }
    
}

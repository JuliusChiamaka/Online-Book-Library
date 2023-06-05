using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Shared.Model
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string ISBN { get; set; }

        //[Required]
        public string? ImageUrl { get; set; }

        [Required]
        public DateTime YearPublished { get; set; }

        [Required]
        public Guid SubCategoryId { get; set; }
    }
}

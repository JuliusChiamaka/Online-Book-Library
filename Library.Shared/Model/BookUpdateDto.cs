using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Shared.Model
{
    public class BookUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime YearPublished { get; set; }

        public Guid SubCategoryId { get; set; }
    }
}

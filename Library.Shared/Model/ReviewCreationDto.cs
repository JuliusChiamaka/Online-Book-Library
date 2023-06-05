using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Shared.Model
{
    public class ReviewCreationDto
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}

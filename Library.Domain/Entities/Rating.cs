using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }

        [Required]
        public string Rate { get; set; }
        public Guid BookId { get; set; }
    }
}

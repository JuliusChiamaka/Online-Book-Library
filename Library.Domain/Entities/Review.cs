using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Message { get; set; }
        public Guid BookId { get; set; }
    }
}

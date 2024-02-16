using System.ComponentModel.DataAnnotations;

namespace Task4.Models
{
    [Display(Name = "Автор")]
    public class Author
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Побатькові")]
        public string? MiddleName { get; set; }

        [Required]
        [Display(Name = "Дата народження")]
        public DateTime Birthday { get; set; }

        public virtual List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
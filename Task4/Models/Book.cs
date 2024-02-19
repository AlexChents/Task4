using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task4.Models
{
    [Display(Name = "Книжка")]
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Назва")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Кількість сторінок")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int CountPages { get; set; }

        [ForeignKey("Genre")]
        [Display(Name = "Жанр")]
        public Guid GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        [ForeignKey("Author")]
        [Display(Name = "Автор")]
        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
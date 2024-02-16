using System.ComponentModel.DataAnnotations;

namespace Task4.Models
{
    [Display(Name = "Жанр")]
    public class Genre
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Назва жанру")]
        public string Name { get; set; }

    }
}

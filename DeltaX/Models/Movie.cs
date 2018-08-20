using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeltaX.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9 !:.\-()]*$", ErrorMessage = "Enter a valid movie name.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$",
         ErrorMessage = "Enter a valid Year.")]
        public int? Year { get; set; }

        [Required]
        [StringLength(300)]
        [DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        public string Poster { get; set; }

        public Producer Producer { get; set; }

        [Display(Name ="Producer")]
        [Required(ErrorMessage ="Select or add producer.")]
        public int ProducerId { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}   
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeltaX.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Only alphabets are allowed.")]
        public string Name { get; set; }

        public string Sex { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [StringLength(300)]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
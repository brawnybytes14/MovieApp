using DeltaX.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DeltaX.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }

        public Actor Actor { get; set; }

        public Producer Producer { get; set; }

        [Display(Name ="Actors")]
        [Required(ErrorMessage = "Select or add at least one actor.")]
        public List<int> ActorIds { get; set; }

        public List<Actor> Actors { get; set; }

        public List<Producer> Producers { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}
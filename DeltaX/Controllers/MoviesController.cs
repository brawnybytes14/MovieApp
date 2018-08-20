using DeltaX.Models;
using DeltaX.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DeltaX.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        public ActionResult Index()
        {
            ViewBag.Title = "Movies";
            var movies = _context.Movies.Include(a => a.Producer).OrderByDescending(a=>a.Id).ToList();
            return View(movies);
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit Movie";
            var movie = _context.Movies.Where(a=>a.Id == Id).FirstOrDefault();
            if (movie == null)
                return HttpNotFound();
            var actors = _context.Actors.ToList();
            var producers = _context.Producers.ToList();
            var actorIds = new List<int>();

            if(movie!=null)
                actorIds = movie.Actors.Select(p => p.Id).ToList();
            
            var vm = new MovieViewModel()
            {
                Movie = movie,
                Actor = new Actor(),
                Producer = new Producer(),
                Actors = actors,
                Producers = producers,
                ActorIds = actorIds
            };

            return View("MovieForm", vm);
        }

        public ActionResult New(int Id=0)
        {
            ViewBag.Title = "Add Movie";
            var actors = _context.Actors.ToList();
            var producers = _context.Producers.ToList();
            var actorIds = new List<int>();
         
            var vm = new MovieViewModel()
            {
                Movie = new Movie(),
                Actor = new Actor(),
                Producer = new Producer(),
                Actors = actors,
                Producers = producers,
                ActorIds = new List<int>()
            };

            return View("MovieForm",vm);
        }

        public ActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(a => a.Producer).Where(a=>a.Id==Id).FirstOrDefault();
            if (movie == null)
                return HttpNotFound();
            return View("Details", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                var actors = _context.Actors.ToList();
                var producers = _context.Producers.ToList();
                var actorIds = new List<int>();

                var vm = new MovieViewModel()
                {
                    Movie = new Movie(),
                    Actor = new Actor(),
                    Producer = new Producer(),
                    Actors = actors,
                    Producers = producers,
                    ActorIds = new List<int>(),
                    File = movieViewModel.File
                };
                return View("MovieForm", vm);
            }
            
            //default image url
            string path = "~/Images/Posters/dummyPoster.png";
            if (movieViewModel.File != null)
            {
                string pic = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(movieViewModel.File.FileName);
                path = System.IO.Path.Combine(Server.MapPath("~/Images/Posters"), pic);
                movieViewModel.File.SaveAs(path);
                path = "~/Images/Posters/" + pic;
            }

            if(movieViewModel.Movie.Id == 0)
            {
                var movie = new Movie()
                {
                    Name = movieViewModel.Movie.Name,
                    Year = movieViewModel.Movie.Year,
                    Plot = movieViewModel.Movie.Plot,
                    Poster = path,
                    ProducerId = movieViewModel.Movie.ProducerId,
                    Actors = _context.Actors.Where(a => movieViewModel.ActorIds.Contains(a.Id)).ToList()
                };
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Include(a=>a.Actors).Single(m => m.Id == movieViewModel.Movie.Id);

                foreach (var child in movieInDb.Actors.ToList())
                    movieInDb.Actors.Remove(child);

                movieInDb.Name = movieViewModel.Movie.Name;
                movieInDb.Year = movieViewModel.Movie.Year;
                movieInDb.Plot = movieViewModel.Movie.Plot;
                if (movieViewModel.File != null)
                {
                    movieInDb.Poster = path;
                }
                movieInDb.ProducerId = movieViewModel.Movie.ProducerId;
                movieInDb.Actors = _context.Actors.Where(a => movieViewModel.ActorIds.Contains(a.Id)).ToList();
            }
           
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Delete(int Id)
        {
            var movie = _context.Movies.Where(a => a.Id == Id).FirstOrDefault();
            if (movie == null)
                return HttpNotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public JsonResult GetActors()
        {
            var actors = _context.Actors.Select(a => new { a.Id, a.Name }).ToList();
            return Json(actors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProducers()
        {
            var producers = _context.Producers.Select(a => new { a.Id, a.Name }).ToList();
            return Json(producers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveActor(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return Json(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
            return Json(producer);
        }

    }
}
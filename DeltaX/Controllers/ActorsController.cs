using DeltaX.Models;
using System.Linq;
using System.Web.Mvc;

namespace DeltaX.Controllers
{
    public class ActorsController : Controller
    {
        private ApplicationDbContext _context;
        public ActorsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New(int Id = 0)
        {
            ViewBag.Title = "Add Actor";
            return View("ActorForm", new Actor());
        }

        public ActionResult Edit(int Id = 0)
        {
            ViewBag.Title = "Edit Actor";
            var actor = _context.Actors.Where(a => a.Id == Id).FirstOrDefault();
            if (actor == null)
                return HttpNotFound();
            return View("ActorForm", actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View("ActorForm", new Actor());
            }
            
            if (actor.Id == 0)
            {
                _context.Actors.Add(actor);
            }
            else
            {
                var actorInDb = _context.Actors.Where(a => a.Id == actor.Id).FirstOrDefault();
                actorInDb.Name = actor.Name;
                actorInDb.Sex = actor.Sex;
                actorInDb.DOB = actor.DOB;
                actorInDb.Bio = actor.Bio;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Actors");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Actors";
            var actors = _context.Actors.OrderByDescending(a => a.Id).ToList();
            return View(actors);
        }

        public ActionResult Details(int Id)
        {
            var actor = _context.Actors.Where(a => a.Id == Id).FirstOrDefault();
            if (actor == null)
                return HttpNotFound();
            return View("Details", actor);
        }

        public ActionResult Delete(int Id)
        {
            var actor = _context.Actors.Where(a => a.Id == Id).FirstOrDefault();
            if (actor == null)
                return HttpNotFound();
            _context.Actors.Remove(actor);
            _context.SaveChanges();

            return RedirectToAction("Index", "Actors");
        }
    }
}
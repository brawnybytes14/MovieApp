using DeltaX.Models;
using System.Linq;
using System.Web.Mvc;
namespace DeltaX.Controllers
{
    public class ProducersController : Controller
    {
        private ApplicationDbContext _context;
        public ProducersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New(int Id = 0)
        {
            ViewBag.Title = "Add Producer";
            return View("ProducerForm", new Producer());
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit Producer";
            var producer = _context.Producers.Where(a => a.Id == Id).FirstOrDefault();
            if (producer == null)
                return HttpNotFound();
            return View("ProducerForm", producer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View("ProducerForm", new Producer());
            }
            
            if (producer.Id == 0)
            {
                _context.Producers.Add(producer);
            }
            else
            {
                var actorInDb = _context.Producers.Where(a => a.Id == producer.Id).FirstOrDefault();
                actorInDb.Name = producer.Name;
                actorInDb.Sex = producer.Sex;
                actorInDb.DOB = producer.DOB;
                actorInDb.Bio = producer.Bio;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Producers");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Producers";
            var Producers = _context.Producers.OrderByDescending(a=>a.Id).ToList();
            return View(Producers);
        }

        public ActionResult Details(int Id)
        {
            var producer = _context.Producers.Where(a => a.Id == Id).FirstOrDefault();
            if (producer == null)
                return HttpNotFound();
            return View("Details", producer);
        }

        public ActionResult Delete(int Id)
        {
            var producer = _context.Producers.Where(a => a.Id == Id).FirstOrDefault();
            if (producer == null)
                return HttpNotFound();
            _context.Producers.Remove(producer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Producers");
        }
    }
}
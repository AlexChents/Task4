using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task4.Helpers;
using Task4.Models;

namespace Task4.Controllers
{
    public class GenreController : Controller
    {
        public DbContextBook _context;

        public GenreController()
        {
            _context = new DbContextBook();
        }

        public ActionResult Index()
        {
            return View(_context.Genres.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            try
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(_context.Genres.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Genre genre)
        {
            try
            {
                var temp = _context.Genres.Find(id);
                if (temp != null)
                {
                    temp.Name = genre.Name;
                }
                _context.Genres.Update(temp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            return View(_context.Genres.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Genre genre)
        {
            try
            {
                _context.Genres.Remove(_context.Genres.Find(id));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

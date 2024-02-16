using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task4.Helpers;
using Task4.Models;

namespace Task4.Controllers
{
    public class BookController : Controller
    {
        public DbContextBook _context;

        public BookController()
        {
            _context = new DbContextBook();
        }

        public ActionResult Index()
        {
            return View(_context.Books.Include(b => b.Genre).Include(b => b.Author).ToList());
        }

        public ActionResult Details(Guid id)
        {
            return View(_context.Books.Include(b => b.Genre).Include(b => b.Author).FirstOrDefault(b => b.Id == id));
        }

        public ActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors.Select(a => new 
                {
                    a.Id, 
                    a.MiddleName, 
                    a.FirstName, 
                    a.LastName, 
                    a.Birthday, 
                    FullName = a.LastName + " " + a.FirstName
                }), "Id", "FullName");
            ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                _context.Books.Add(book);
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
            ViewBag.Authors = new SelectList(_context.Authors.Select(a => new
            {
                a.Id,
                a.MiddleName,
                a.FirstName,
                a.LastName,
                a.Birthday,
                FullName = a.LastName + " " + a.FirstName
            }), "Id", "FullName", _context.Books.Find(id).Author);
            ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name", _context.Books.Find(id).Genre);
            return View(_context.Books.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Book book)
        {
            try
            {
                var temp = _context.Books.Find(id);
                if (temp != null)
                {
                    temp.AuthorId = book.AuthorId;
                    temp.CountPages = book.CountPages;
                    temp.GenreId = book.GenreId;
                    temp.Name = book.Name;
                }
                _context.Books.Update(temp);
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
            Book temp = _context.Books.Find(id);
            if (temp != null)
            {
                ViewBag.Author = _context.Authors.Find(temp.AuthorId).LastName + " " + _context.Authors.Find(temp.AuthorId).FirstName;
                ViewBag.Genre = _context.Genres.Find(temp.GenreId).Name;
            }
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Book book)
        {
            try
            {
                _context.Books.Remove(_context.Books.Find(id));
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task4.Models;

namespace Task4.Controllers
{
    public class AuthorController : Controller
    {
        public DbContextBook _context;

        public AuthorController()
        {
            _context = new DbContextBook();
        }
        
        public IActionResult Index()
        {
            return View(_context.Authors.Include(a => a.Books).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            try
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(Guid id)
        {
            var booksAuthor = _context.Books.Include(b => b.Author).Include(b => b.Genre).Where(b => b.AuthorId == id).ToList();
            return View(booksAuthor);
        }

        public ActionResult Edit(Guid id)
        {
            Author author = _context.Authors.Find(id);
            ViewBag.AuthorBooks = _context.Books.Select(b => b.AuthorId == id).ToList();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Author author)
        {
            try
            {
                var temp = _context.Authors.Find(id);
                if (temp != null)
                {
                    temp.MiddleName = author.MiddleName;
                    temp.LastName = author.LastName;
                    temp.FirstName = author.FirstName;
                    temp.Birthday = author.Birthday;
                }
                _context.Authors.Update(temp);
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
            return View(_context.Authors.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Author author)
        {
            try
            {
                _context.Authors.Remove(_context.Authors.Find(id));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddBook(Guid id)
        {
            ViewData["AuthorId"] = id;
            ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name");
            return PartialView("_BookPartialAddView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(Guid id, [Bind("Name", "CountPages", "Genre")]Book book)
        {
            try
            {
                _context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    Name = book.Name,
                    AuthorId = id,
                    GenreId = book.Genre.Id,
                    CountPages = book.CountPages
                });
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

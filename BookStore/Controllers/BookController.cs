using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        public BookAppDBContext _dbContext;
        public BookController (BookAppDBContext context)
        {
            _dbContext = context;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Books.ToList());
        }

        public IActionResult CreateUpDate(int? id)
        {
            Book book = new Book();
            if (id == null || id == 0)
            {
                return View(book);
            }
            //update
            else
            {
                book = _dbContext.Books.Find(id);
                return View(book);
            }
        }
        [HttpPost]
        public IActionResult CreateUpdate(Book book)
        {
            if(ModelState.IsValid)
            {
                if (book.Id == 0 || book.Id == null)
                {
                    _dbContext.Books.Add(book);
                    TempData["success"] = "Book Created successfully!";
                }
                else
                {
                    _dbContext.Books.Update(book);
                    TempData["success"] = "Book Updated successfully!";
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Book book = _dbContext.Books.Find(id);
            if (book == null)
                return NotFound();
            return Delete(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _dbContext.Books.Remove(book);
            TempData["success"] = "Book Deleted Successfully!";
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

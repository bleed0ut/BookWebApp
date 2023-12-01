using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Responsitory;
using BookStore.Responsitory.iRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Book> books = _unitOfWork.BookRepository.GetAll("Category").ToList();
            return View(books);
        }

        public IActionResult CreateUpDate(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Book = new Book()
            };
            if (id == null || id == 0)
            {
                return View(bookVM);
            }
            //update
            else
            {
                bookVM.Book = _unitOfWork.BookRepository.Get(b => b.Id == id);
                return View(bookVM);
            }
        }
        [HttpPost]
        public IActionResult CreateUpdate(BookVM bookVM)
        {
            if(ModelState.IsValid)
            {
                if (bookVM.Book.Id == 0 || bookVM.Book.Id == null)
                {
                    _unitOfWork.BookRepository.Add(bookVM.Book);
                    TempData["success"] = "Book Created successfully!";
                }
                else
                {
                    _unitOfWork.BookRepository.Update(bookVM.Book);
                    TempData["success"] = "Book Updated successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                BookVM bookVMNew = new BookVM()
                {
                    Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    Book = new Book()
                };
                return View(bookVMNew);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Book book = _unitOfWork.BookRepository.Get(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Delete(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _unitOfWork.BookRepository.Delete(book);
            TempData["success"] = "Book Deleted Successfully!";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}

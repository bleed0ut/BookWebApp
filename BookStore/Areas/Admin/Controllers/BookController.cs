using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Responsitory;
using BookStore.Responsitory.iRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
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
        public IActionResult CreateUpdate(BookVM bookVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //handle book's image
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwrootPath, @"images\books");

                    if (!string.IsNullOrEmpty(bookVM.Book.ImageUrl))
                    {
                        var oldImagepath = Path.Combine(wwwrootPath, bookVM.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImageUrl = @"\images\books\" + fileName;
                }

                //create or update
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

using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private BookAppDBContext _dbContext;
        public CategoryController(BookAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.Description)
            {
                ModelState.AddModelError("Name", "Name cannot be equal to description");
            }
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Created Successfully!";
                return Redirect("Index");
            }
            
            return View();
        }
        
        public IActionResult Edit(int? id) {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category) {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            Category category = _dbContext.Categories.Find(id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category) {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}

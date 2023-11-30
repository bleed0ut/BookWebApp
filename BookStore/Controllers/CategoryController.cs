using BookStore.Data;
using BookStore.Models;
using BookStore.Responsitory.iRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> categories = CategoryRepository.GetAll().ToList();
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
                CategoryRepository.Add(category);
                CategoryRepository.Save();
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

            Category? category = CategoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category) {
            CategoryRepository.Update(category);
            CategoryRepository.Save();
            TempData["success"] = "Category Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            Category category = CategoryRepository.Get(c => c.Id == id);    
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category) {
            CategoryRepository.Delete(category);
            CategoryRepository.Save();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}

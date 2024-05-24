using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.DataAccess.Data;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        // 33 , give me a implementation of ... ApplicationDBContext (dependency injection from Program.cs): you have that because i registered it in to services
        private readonly ApplicationDBContext _db; 
        public CategoryController(ApplicationDBContext db) // whatever implementatioon we get in the constructor we asign it to the <_db> local varibale.
        {
            _db = db;
        } 
        public IActionResult Index()
        {   // RETRIVE
            List<Category> objCategoryList = _db.Categories.ToList(); 

            return View(objCategoryList); 
        }
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost] 
        public IActionResult Create(Category obj) 
        {
             // custom validation:
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","Display order can't match the name"); 
            }
            if (ModelState.IsValid)
            { 
                _db.Categories.Add(obj); 
                _db.SaveChanges(); 

                TempData["success"] = "Category created sucessfuly"; 

                return RedirectToAction("Index", "Category"); 

            }
            return View(); 

        }

        public IActionResult Edit(int? id) 
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
             Category categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); 
        }

        // Post Action
        [HttpPost] 
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            { 
                _db.Categories.Update(obj); 
                _db.SaveChanges();

                TempData["success"] = "Category updated sucessfuly";

                return RedirectToAction("Index", "Category"); 
            }
            return View(); 

        }

        public IActionResult Delete(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // Post Action
        [HttpPost, ActionName("Delete")] 
        public IActionResult DeletePost(int? id/*Category obj*/) 
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Category deleted sucessfuly"; 

            return RedirectToAction("Index", "Category");
        }


    }
}

using ConvenientStoreWeb.Data;
using ConvenientStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientStoreWeb.Controllers
{
    public class ProductController : Controller
    {
        public readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            return View(products);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if(product.Id.ToString()==product.ProductName)
            {
                ModelState.AddModelError("CustomError","ID and Name Can't be the Same");
            }
            if(ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["success"] = "Product Has been Created Successfully";
                return RedirectToAction("Index");
            }
            
            return View(product);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null||id==0)
            {
                return NotFound();
            }
            var ProductFromDb=_db.Products.Find(id);
            if(ProductFromDb==null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {       
            if (product.Id.ToString() == product.ProductName)
            {
                ModelState.AddModelError("CustomError", "ID and Name Can't be the Same");
            }
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["success"] = "Product Has been Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if(id==null||id==0)
            {
                return NotFound();
            }
            var productFromDb = _db.Products.Find(id);
            if(productFromDb==null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["success"]= "Product Has been Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

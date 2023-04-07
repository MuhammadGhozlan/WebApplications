using Microsoft.AspNetCore.Mvc;
using MoviesStoreWeb.Data;
using MoviesStoreWeb.Models;

namespace MoviesStoreWeb.Controllers
{
    public class MovieController : Controller
    {
        public readonly ApplicationDbContext _db;
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Movie> obj = _db.Movies;
            return View(obj);
        }

        //GET
        public IActionResult Create() 
        {            
            return View();
        }
        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (movie.MovieName==movie.Id.ToString()) 
            {
                ModelState.AddModelError("CustomeError","Movie ID and Movie Name can't be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Movies.Add(movie);
                _db.SaveChanges();
                TempData["success"] = "Movie Created Successfully";
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var MovieFromDb=_db.Movies.Find(id);
            if (MovieFromDb==null)
            {
                return NotFound();
            }
            
            return View(MovieFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (movie.MovieName==movie.Id.ToString()) 
            {
                ModelState.AddModelError("CustomError","Movie ID and Movie Name can't be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Movies.Update(movie);
                _db.SaveChanges();
                TempData["success"] = "Movie Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var MovieFromDb= _db.Movies.Find(id);
            if (MovieFromDb==null)
            {
                return NotFound();
            }
            return View(MovieFromDb);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Movie movie) 
        {
           
                _db.Movies.Remove(movie);
                _db.SaveChanges();
                TempData["success"] = "Movie is deleted successfully";
                return RedirectToAction("Index");
           
        }
    }
}

   

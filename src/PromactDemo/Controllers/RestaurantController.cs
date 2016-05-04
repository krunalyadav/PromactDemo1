using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using PromactDemo.Models;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PromactDemo.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _context;

        public RestaurantController()
        {
            _context = new RestaurantDbContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_context.Restaurant.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurant.Add(restaurant);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Something went wrong. Please try after some time");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Restaurant.FirstOrDefault(x => x.Id == id) ?? new Restaurant());
        }

        [HttpPost]
        public IActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                var editedRestaurant = _context.Restaurant.FirstOrDefault(x => x.Id == restaurant.Id);
                if (editedRestaurant != null)
                {
                    editedRestaurant.Name = restaurant.Name;
                    editedRestaurant.Cuisine = restaurant.Cuisine;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Restaurant not found");
                return View();
            }
            ModelState.AddModelError("", "Something went wrong. Please try after some time");
            return View();
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var restaurant = _context.Restaurant.FirstOrDefault(x => x.Id == id);
            if (restaurant != null)
            {
                _context.Remove(restaurant);
                _context.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using PromactDemo.Models;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PromactDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _securityManager;
        private readonly SignInManager<User> _loginManager;

        public AccountController(UserManager<User> secMgr, SignInManager<User> loginManager)
        {
            _securityManager = secMgr;
            _loginManager = loginManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                ViewData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ViewData["ReturnUrl"])))
                    {
                        return Redirect(Convert.ToString(ViewData["ReturnUrl"]));
                    }
                    return RedirectToAction(nameof(RestaurantController.Index), nameof(Restaurant));
                }
                return View();
            }
            ModelState.AddModelError("", "Invalid Data");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = register.UserName,
                    Email = register.Email
                };
                var result = await _securityManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _loginManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(RestaurantController.Index), nameof(Restaurant));
                }
            }
            ModelState.AddModelError("", "Invalid Model");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _loginManager.SignOutAsync();
            return RedirectToAction(nameof(RestaurantController.Index), nameof(Restaurant));
        }
    }
}
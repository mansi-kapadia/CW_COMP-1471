using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Controllers
{
    public class AccountController : Controller
    {

        private static IRoleService roleService;
        private static IUserService userService;

        public AccountController(IRoleService _roleService, IUserService _userService)
        {
            roleService = _roleService;
            userService = _userService;
        }

        // use this link
        //http://www.mtutorial.com/asp.net-core-login-logout-registration-example

        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (userService.CheckPassword(model))
            {
                // Create a cookie with username (or session token)
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                    HttpOnly = true,
                    Secure = true, 
                    IsEssential = true
                };

                Response.Cookies.Append("UserSession", model.Username, cookieOptions);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid credentials";
            return View();

        }

        // Display the Register Form
        [HttpGet]
        public IActionResult Register()
        {
            var model = userService.GetRegisterModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = userService.RegisterUser(model);
                if (!isRegistered)
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    model.Roles = new SelectList(userService.GetRegisterModel().Roles, "Value", "Text");
                    return View(model);
                }

                return RedirectToAction("Login"); 
            }

            // Reload Roles if validation fails
            model.Roles = new SelectList(userService.GetRegisterModel().Roles, "Value", "Text");

            return View(model);
        }

        public IActionResult Logout()
        {
            // Remove the cookie
            Response.Cookies.Delete("UserSession"); 
            return RedirectToAction("Login", "Account");
        }
    }
}

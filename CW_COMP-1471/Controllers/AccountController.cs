using CW_COMP_1471.Models;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class AccountController : Controller
    {

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
            //if (ModelState.IsValid)
            //{
            //    var userdetails = await _context.Userdetails
            //    .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
            //    if (userdetails == null)
            //    {
            //        ModelState.AddModelError("Password", "Invalid login attempt.");
            //        return View("Index");
            //    }
            //    HttpContext.Session.SetString("userId", userdetails.Name);

            //}
            //else
            //{
              return View();
            //}
            //return RedirectToAction("Index", "Home");
        }

    }
}

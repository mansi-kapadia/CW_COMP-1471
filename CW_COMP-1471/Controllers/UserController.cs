using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Views.Home
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(UserServices.GetUsers());
        }

        // Show Add User Form
        public ActionResult CreateUser()
        {
            var model = new User
            {
                Roles = new SelectList(RoleService.GetRoles(), "Id", "RoleName")
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                user.Roles = new SelectList(RoleService.GetRoles(), "Id", "RoleName");
                return View("CreateUser", user);
            }

            UserServices.AddUser(user);
            return RedirectToAction("Index");
        }
        
        public ActionResult EditUser(int id)
        {
            ViewBag.Roles = new SelectList(RoleService.GetRoles(), "Id", "RoleName");
            var user = UserServices.GetUserById(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                UserServices.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            UserServices.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}

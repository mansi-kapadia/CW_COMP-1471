using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Controllers
{
    public class UserController : Controller
    {
        private static IUserService userService;
        private static IRoleService roleService;

        public UserController(IUserService _userService, IRoleService _roleService)
        {
            userService = _userService; 
            roleService = _roleService;
        }

        public IActionResult Index()
        {
            return View(userService.GetUsers());
        }

        // Show Add User Form
        public ActionResult CreateUser()
        {            
            var model = new User
            {
                Roles = new SelectList(roleService.GetRoles(true) ?? new List<Role>(), "Id", "RoleName")
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
                return View("CreateUser", user);
            }

            userService.AddUser(user);
            return RedirectToAction("Index");
        }
        
        public ActionResult EditUser(int id)
        {
            var user = userService.GetUserById(id);
            
            if (user == null)
                return HttpNotFound();

            user.Roles = new SelectList(roleService.GetRoles(true) ?? new List<Role>(), "Id", "RoleName");

            return View(user);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("EditUser", user);
            }

            userService.UpdateUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}

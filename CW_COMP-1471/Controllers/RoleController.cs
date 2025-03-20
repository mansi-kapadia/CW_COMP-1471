using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class RoleController : Controller
    {
        private static IRoleService roleService;

        public RoleController(IRoleService _roleService)
        {
            // dependency injection
            roleService = _roleService;
        }

        //show list of roles
        public ActionResult Index()
        {
            return View(roleService.GetRoles(false));
        }

        // Show Add role Form
        public ActionResult CreateRole()
        {
            return View();
        }

        // save a new role
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("CreateRole", role);
            }

            roleService.Add(role);
            return RedirectToAction("Index");
        }

        // open edit role page
        public ActionResult EditRole(int id)
        {
            var role = roleService.GetById(id);
            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // save updated role 
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                roleService.Update(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

    }
}

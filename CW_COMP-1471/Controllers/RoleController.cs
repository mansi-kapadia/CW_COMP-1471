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
            roleService = _roleService;
        }

        public ActionResult Index()
        {
            return View(roleService.GetRoles());
        }

        // Show Add User Form
        public ActionResult CreateRole()
        {
            return View();
        }


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

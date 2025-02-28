using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class PackageController : Controller
    {
        private static IPackageService packageService;
        public PackageController(IPackageService _packageService)
        {
            packageService = _packageService;
        }

        public IActionResult Index()
        {
            var packages = packageService.GetAllPackages();
            return View(packages);
        }

        public IActionResult CreatePackage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                packageService.Add(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        public IActionResult EditPackage(int id)
        {
            var package = packageService.GetById(id);
            if (package == null)
                return NotFound();

            return View(package);
        }

        [HttpPost]
        public IActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                packageService.Update(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        public IActionResult Delete(int id)
        {
            var package = packageService.GetById(id);
            if (package == null)
                return NotFound();

            packageService.DeletePackage(id);
            return RedirectToAction("Index");
        }
       
    }
}

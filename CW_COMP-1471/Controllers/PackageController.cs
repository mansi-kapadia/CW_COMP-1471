using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            var packages = PackageService.GetAllPackages();
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
                PackageService.Add(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        public IActionResult EditPackage(int id)
        {
            var package = PackageService.GetById(id);
            if (package == null)
                return NotFound();

            return View(package);
        }

        [HttpPost]
        public IActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                PackageService.Update(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        public IActionResult Delete(int id)
        {
            var package = PackageService.GetById(id);
            if (package == null)
                return NotFound();

            PackageService.DeletePackage(id);
            return RedirectToAction("Index");
        }
       
    }
}

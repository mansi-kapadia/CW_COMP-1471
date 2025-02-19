using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Controllers
{
    public class PricingController : Controller
    {
        public IActionResult Index()
        {
            return View(PricingService.GetPricings());
        }

        // Show Add Pricing band Form
        public ActionResult CreatePricing()
        {
            var model = new Pricing
            {
                plays = new SelectList(PlayService.GetAllPlays(), "Playid", "Title")
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(Pricing Pricing)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("CreatePricing", Pricing);
            }

            PricingService.Add(Pricing);
            return RedirectToAction("Index");
        }

        public ActionResult EditPricing(int id)
        {
            var pricing = PricingService.GetById(id);
            if (pricing == null)
                return HttpNotFound();
            pricing.plays = new SelectList(PlayService.GetAllPlays(), "Playid", "Title");
            return View(pricing);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(Pricing Pricing)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();
                ViewBag.Errors = errors;
                return View("EditPricing", Pricing);
            }

            PricingService.Update(Pricing);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            PricingService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Controllers
{
    public class PricingController : Controller
    {
        private static IPricingService pricingService;
        private static IPlayService playService;
        public PricingController(IPricingService _pricingService, IPlayService _playService)
        {
            pricingService = _pricingService;
            playService = _playService;
        }

        public IActionResult Index()
        {
            return View(pricingService.GetPricings());
        }

        // Show Add Pricing band Form
        public ActionResult CreatePricing()
        {
            var model = new PricingBand();
            model.Plays = new SelectList(playService.GetAllPlays() ?? new List<Play>(), "PlayId", "Title");
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(PricingBand Pricing)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("CreatePricing", Pricing);
            }

            pricingService.Add(Pricing);
            return RedirectToAction("Index");
        }

        public ActionResult EditPricing(int id)
        {
            var pricing = pricingService.GetById(id);
            if (pricing == null)
                return HttpNotFound();     
            pricing.Plays = new SelectList(playService.GetAllPlays() ?? new List<Play>(), "PlayId", "Title");
            return View(pricing);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(PricingBand Pricing)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();
                ViewBag.Errors = errors;
                return View("EditPricing", Pricing);
            }

            pricingService.Update(Pricing);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            pricingService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

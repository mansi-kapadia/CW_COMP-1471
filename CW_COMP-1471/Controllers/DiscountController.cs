using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class DiscountController : Controller
    {
        private static IDiscountService discountService;
        public DiscountController(IDiscountService _discountService)
        {
            discountService = _discountService;
        }

        public IActionResult Index()
        {
            return View(discountService.GetAllDiscounts());
        }

        public ActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Discount discount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View("CreateDiscount", discount);
            }

            discountService.Add(discount);
            return RedirectToAction("Index");
        }

        public ActionResult EditDiscount(int id)
        {
            var discount = discountService.GetById(id);
            if (discount == null)
                return NotFound();

            return View(discount);
        }

        [HttpPost]
        public ActionResult Edit(Discount discount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View("EditDiscount", discount);
            }

            discountService.Update(discount);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            discountService.DeleteDiscount(id);
            return RedirectToAction("Index");
        }
    }
}

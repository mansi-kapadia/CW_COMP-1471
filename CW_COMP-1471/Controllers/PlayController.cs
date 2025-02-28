using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class PlayController : Controller
    {
        private static IPlayService playService;

        public PlayController(IPlayService _playService)
        {
            playService = _playService;
        }

        public IActionResult Index()
        {
            return View(playService.GetAllPlays());
        }

        // Show Add Play Form
        public ActionResult CreatePlay()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Play Play)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("CreatePlay", Play);
            }

            playService.Add(Play);
            return RedirectToAction("Index");
        }

        public ActionResult EditPlay(int id)
        {
            var Play = playService.GetById(id);
            if (Play == null)
                return HttpNotFound();

            return View(Play);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(Play Play)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.Errors = errors;
                return View("EditPlay", Play);
            }
            playService.Update(Play);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            playService.DeletePlay(id);
            return RedirectToAction("Index");
        }
    }
}

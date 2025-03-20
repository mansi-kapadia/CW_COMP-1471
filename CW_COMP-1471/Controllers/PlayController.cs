using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    [ApiController]
    [Route("api/play")]
    public class PlayController : Controller
    {
        private static IPlayService playService;

        public PlayController(IPlayService _playService)
        {
            // dependency injection
            playService = _playService;
        }

        // show list of plays
        public IActionResult Index()
        {
            return View(playService.GetAllPlays());
        }

        // Show Add Play Form
        [HttpGet("Create")]
        public ActionResult CreatePlay()
        {
            return View();
        }


        // save a new play
        [HttpPost("Create")]
        public ActionResult Create([FromForm]Play Play)
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

        // open edit play page 
        [HttpGet("Edit")]
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

        // save updated play details
        [HttpPost("Edit")]
        public ActionResult Edit([FromForm] Play Play)
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

        //delete play by id
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            playService.DeletePlay(id);
            return RedirectToAction("Index");
        }

        [HttpGet("PlayDetails/{PlayId}")]
        public ActionResult PlayDetails(int PlayId)
        {
            var model = playService.GetPlayDetails(PlayId);
            return View(model);
        }
    }
}

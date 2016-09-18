using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActorLibrary.Controllers
{
    public class HomeController : Controller
    {
        private ActorContext _db = new ActorContext();

        public ActionResult Index()
        {
            return View(_db.Actors.
                //OrderBy(m => m.LastName).
                ToList());
        }

        public ActionResult PlayAudio(string audioUrl)
        {
            var file = Server.MapPath(audioUrl);
            return File(file, "audio/mp3");
        }

        public ActionResult Details(int? id)
        {
            var actor = _db.Actors.Find(id);

            return View(actor);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
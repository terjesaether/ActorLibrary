using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
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
        public ActionResult Search(string searchString)
        {
            var result = new List<Actor>();

            var actors = from a in _db.Actors
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(a => a.FirstName.Contains(searchString)
                                       || a.LastName.Contains(searchString));
            }

            result = actors.ToList();

            if (actors.Count() == 0)
            {
                ViewBag.SearchMessage = "Ingen resultater";
            }
            return View(result);
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var actor = _db.Actors.Find(id.Value);
            if (actor == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EditCreateViewModel();
            viewModel.Actor = actor;

            //var selectList = viewModel.SetSelectedValue(viewModel.GenderItems, viewModel.Actor.Gender)
            var allGenders = _db.Genders.Select(f => new SelectListItem
            {
                Value = f.GenderId.ToString(),
                Text = f.GenderName,
                Selected = f.GenderName == viewModel.Actor.ActorGenderId
            });


            viewModel.GenderItems = allGenders.ToList();

            return View(viewModel);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(EditCreateViewModel viewModel, HttpPostedFileBase uploadImg)
        {

            if (uploadImg != null)
            {
                // TO-DO Sjekk for identisk navn
                string pathToSaveImg = Server.MapPath("~/img/");
                string filename = "profile" + viewModel.Actor.Filename + ".jpg";
                string fullFilename = Path.Combine(pathToSaveImg, filename);
                uploadImg.SaveAs(fullFilename);
                viewModel.Actor.ImgUrl = "/img/" + filename.ToString();
            }

            try
            {

                //if (!string.IsNullOrEmpty(viewModel.Actor.ActorGenderName))
                //{
                //    var genderId = _db.Genders.Find(Convert.ToInt32(viewModel.Gender));
                //    viewModel.Actor.ActorGenderName = genderId.GenderName.ToString();
                //}

                _db.Entry(viewModel.Actor).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Actor/Create
        public ActionResult Create()
        {

            var viewModel = new EditCreateViewModel();

            var allGenders = _db.Genders.Select(f => new SelectListItem
            {
                Value = f.GenderId.ToString(),
                Text = f.GenderName
            });

            viewModel.GenderItems = allGenders.ToList();

            return View(viewModel);
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "LastName, FirstName, Address, Phone, Mail, Comment, BirthDate")]Actor actor, string ActorGenderId, string audioTitle, HttpPostedFileBase uploadAudio, HttpPostedFileBase uploadImg)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string pathToSaveAudio = Server.MapPath("~/audio/");
                    string pathToSaveImg = Server.MapPath("~/img/");

                    if (uploadImg != null && uploadImg.ContentLength > 0)
                    {
                        // TO-DO Sjekk for identisk navn

                        string filename = "profile" + actor.Filename + ".jpg";
                        string fullFilename = Path.Combine(pathToSaveImg, filename);
                        uploadImg.SaveAs(fullFilename);
                        actor.ImgUrl = "/img/" + filename.ToString();
                    }


                    if (uploadAudio != null && uploadAudio.ContentLength > 0)
                    {
                        // TO-DO Sjekk for identisk navn

                        string filename = actor.Filename + "_" + audioTitle.Replace(" ", "_") + "_" + actor.ActorId + ".mp3";
                        string fullFilename = Path.Combine(pathToSaveAudio, filename);
                        uploadAudio.SaveAs(fullFilename);
                        var vt = new VoiceTest();
                        vt.VoiceTestTitle = audioTitle;
                        vt.VoiceTestUrl = "/audio/" + filename.ToString();
                        actor.VoiceTests.Add(vt);
                        _db.VoiceTests.Add(vt);
                    }

                    //if (!string.IsNullOrEmpty(ActorGenderId))
                    //{
                    //    var gender = _db.Genders.Find(Convert.ToInt32(ActorGenderId));
                    //    actor.ActorGenderId = gender.GenderName.ToString();
                    //}


                    _db.Actors.Add(actor);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home", null);
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(actor);
        }

        private void PopulateGenderDownList(object selectedGender = null)
        {
            var genderQuery = from g in _db.Genders
                              orderby g.GenderName
                              select g;
            ViewBag.GenderId = new SelectList(genderQuery, "GenderId", "GenderName", selectedGender);
        }
    }
}
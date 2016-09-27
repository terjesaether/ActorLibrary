using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        private List<SelectListItem> sortByList = new List<SelectListItem>
        {
                new SelectListItem { Value = "FirstName", Text = "Fornavn"},
                new SelectListItem { Value = "LastName", Text = "Etternavn"},
                new SelectListItem { Value = "AddedDate", Text = "Lagt til dato"},
                new SelectListItem { Value = "Age", Text = "Alder"}
        };

        public ActionResult Index()
        {
            ViewBag.sortList = sortByList;

            return View(_db.Actors.
                ToList());
        }

        [HttpPost]
        public ActionResult Index(string sortBy)
        {

            Func<Actor, Object> orderByFunc = null;

            if (sortBy == "LastName")
                orderByFunc = item => item.LastName;
            else if (sortBy == "FirstName")
                orderByFunc = item => item.FirstName;
            else if (sortBy == "AddedDate")
                orderByFunc = item => item.AddedDate;
            else if (sortBy == "Age")
                orderByFunc = item => item.Age;

            ViewBag.sortList = sortByList;

            return View(_db.Actors.
                OrderByDescending(orderByFunc).
                ToList());
        }

        public ActionResult PlayAudio(string audioUrl)
        {
            var file = Server.MapPath(audioUrl);
            return File(file, "audio/mp3");
        }

        public ActionResult Details(int? id)
        {
            var actor = _db.Actors.Find(id.Value);
            var allGenders = _db.Genders.ToList();

            var viewModel = new ActorViewModel
            {
                Actor = actor
            };

            var gender = allGenders.First(g => g.GenderId == Convert.ToInt32(actor.ActorGenderId));
            ViewBag.GenderName = gender.GenderName;
            return View(viewModel);
        }


        public ActionResult Search(string searchString)
        {
            var result = new List<Actor>();
            var genderNames = new List<string>();
            var allGenders = _db.Genders.ToList();


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

            foreach (var actor in result)
            {
                if (!string.IsNullOrEmpty(actor.ActorGenderId))
                {
                    var gender = allGenders.First(g => g.GenderId == Convert.ToInt32(actor.ActorGenderId));
                    genderNames.Add(gender.GenderName);
                }
                else
                {
                    genderNames.Add("N.A.");
                }

            }

            ViewBag.GenderNames = genderNames;
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

                    if (string.IsNullOrEmpty(ActorGenderId))
                    {
                        //var findGender = _db.Genders.Find(Convert.ToInt32(ActorGenderId));
                        actor.ActorGenderId = "3";
                    }

                    actor.ActorGenderId = ActorGenderId;

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

        public ActionResult AddAudioFiles(int? id)
        {
            if (id == null)
            {

                return HttpNotFound();
                //return View("Index");
            }
            var actor = _db.Actors.Find(id);
            return View(actor);
        }

        [HttpPost]
        public ActionResult AddAudioFiles(int? id, string title, HttpPostedFileBase audioFile)
        {
            var actor = _db.Actors.Find(id);
            var vt = new VoiceTest();

            if (string.IsNullOrEmpty(title))
            {
                title = "Ingen tittel";
            }

            if (audioFile.ContentType != "audio/mp3" || audioFile.ContentLength > 100000000)
            {
                ViewBag.Error = "<strong style='color:red;'>Feil filtype eller for stor fil</strong>";
                return View(actor);
            }

            if (ModelState.IsValid)
            {
                string pathToSaveAudio = Server.MapPath("~/audio/");

                if (audioFile != null & audioFile.ContentLength > 0)
                {
                    string filename = actor.Filename.Replace(" ", "_") + "_" + title.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".mp3";
                    string fullFilename = Path.Combine(pathToSaveAudio, filename);
                    // Lagrer fila på disk
                    audioFile.SaveAs(fullFilename);
                    vt.VoiceTestTitle = actor.FullName + " - " + title;
                    vt.VoiceTestUrl = "/audio/" + filename.ToString();
                    actor.VoiceTests.Add(vt);
                    _db.VoiceTests.Add(vt);

                }
            }
            _db.SaveChanges();
            return View(actor);
        }


        [HttpPost]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            try
            {
                var actor = _db.Actors.Find(id.Value);
                _db.Actors.Remove(actor);
                _db.SaveChanges();
                return Json(new { ok = true, newurl = Url.Action("Index") });
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                //return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                return Json(new { ok = false, message = ex.Message });
            }


        }

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int? id)
        //{
        //    return View();
        //}

        private void PopulateGenderDownList(object selectedGender = null)
        {
            var genderQuery = from g in _db.Genders
                              orderby g.GenderName
                              select g;
            ViewBag.GenderId = new SelectList(genderQuery, "GenderId", "GenderName", selectedGender);
        }

        private string GetGenderNames(int? id, Actor actor, List<Gender> genders)
        {

            var gender = genders.FirstOrDefault(g => g.GenderId == Convert.ToInt32(actor.ActorGenderId));

            return gender.GenderName;

        }
    }
}
using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ActorLibrary.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private ActorContext _db = new ActorContext();

        private CommonOperations comOp = new CommonOperations();
        private SortingOperations sortOp = new SortingOperations();
        private ActorRepository actorRepo = new ActorRepository();
        private UploadFileOperations fileOperations = new UploadFileOperations();

        string profileImgPath = "/Images/profiles/";

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.sortList = comOp.GetSortedbyList();
            ViewBag.dialectsList = comOp.GetDialectsList();
            ViewBag.genderList = comOp.GetGenderList();
            ViewBag.agesList = comOp.GetAgeDefinitonsList();

            return View(_db.Actors.
                ToList());
        }


        //[HttpPost]
        //public ActionResult Index(string sortBy)
        //{

        //    //ViewBag.sortList = comOp.GetSortedbyList();
        //    //ViewBag.dialectsList = comOp.GetDialectsList();
        //    //ViewBag.genderList = comOp.GetGenderList();
        //    //ViewBag.agesList = comOp.GetAgeDefinitonsList();

        //    //return View(sortOp.SortBy(sortBy));
        //    return View();
        //}

        [Route("Home/Sort")]
        public ActionResult SortTheActors(string sortBy, string fromAge, string toAge, string[] sortByDialect, string sortByGender, string sortByAgeDefinition)
        {

            //var FromAge = Request.Form["fromAge"];
            //var ToAge = Request.Form["toAge"];
            //var sortByWhat = Request.Form["sortBy"];
            //var sortByDialect1 = Request.Form["sortbyDialect"];

            //var fromAgeRange = _db.AgeRanges.Find(Convert.ToInt32(fromAge));
            //var toAgeRange = _db.AgeRanges.Find(Convert.ToInt32(toAge));

            var actors = sortOp.SortActors(sortBy, fromAge, toAge, sortByDialect, sortByGender, sortByAgeDefinition);

            ViewBag.sortList = comOp.GetSortedbyList();
            ViewBag.dialectsList = comOp.GetDialectsList();
            ViewBag.genderList = comOp.GetGenderList();
            ViewBag.agesList = comOp.GetAgeDefinitonsList();

            return View("Index", actors);
        }

        public ActionResult Details(int? id)
        {
            var actor = _db.Actors.Find(id.Value);
            var viewModel = new ActorViewModel(id.Value);
            viewModel.Actor = actor;

            //var allGenders = _db.Genders.ToList();
            var gender = _db.Genders.FirstOrDefault(g => g.GenderId == actor.ActorGenderId);

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
                ViewBag.SearchMessage = "Ingen resultater ...";
            }

            foreach (var actor in result)
            {
                if (actor.ActorGenderId > 0)
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
            var viewModel = new EditCreateViewModel(id.Value);
            viewModel.Actor = actor;

            //var allGenders = _db.Genders.Select(f => new SelectListItem
            //{
            //    Value = f.GenderId.ToString(),
            //    Text = f.GenderName,
            //    Selected = f.GenderName == viewModel.Actor.ActorGenderId
            //});

            //viewModel.GenderList = allGenders.ToList();

            //var allDialects = _db.DialectNames
            //    .Select(s => new
            //    {
            //        s.DialectListName,
            //        s.DialectListId,
            //        Selected = _db.ActorDialects.Where(d => d.ActorId == id.Value && d.DialectListId == s.DialectListId)
            //        .Count() > 0
            //    })
            //    .ToList();

            //var dialectDropDown = new List<SelectListItem>();
            //foreach (var item in allDialects)
            //{
            //    dialectDropDown.Add(new SelectListItem
            //    {
            //        Text = item.DialectListName.ToString(),
            //        Value = item.DialectListId.ToString(),
            //        Selected = item.Selected
            //    });

            //}

            ViewBag.DialectListId = viewModel.DialectList;
            ViewBag.ActorGenderId = viewModel.GenderList;

            return View(viewModel);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(EditCreateViewModel viewModel, HttpPostedFileBase uploadImg, string[] DialectListId, string ActorGenderId)
        {
            string pathToSaveImg = Server.MapPath("~" + profileImgPath);

            //var actor = viewModel.Actor;
            int id = viewModel.Actor.ActorId;
            Actor actor = _db.Actors.Find(id);

            actor.FirstName = viewModel.Actor.FirstName;
            actor.LastName = viewModel.Actor.LastName;
            actor.Mail = viewModel.Actor.Mail;
            actor.Phone = viewModel.Actor.Phone;
            actor.BirthDate = viewModel.Actor.BirthDate;

            actor.Dialects.Clear();

            if (string.IsNullOrEmpty(ActorGenderId))
            {
                ActorGenderId = "1";
            }
            else
            {
                actor.ActorGenderId = int.Parse(ActorGenderId);
            }

            foreach (var item in _db.ActorDialects)
            {
                if (item.ActorId == id)
                {
                    _db.Entry(item).State = EntityState.Deleted;
                }
            }

            if (DialectListId != null)
            {
                foreach (var d in DialectListId)
                {
                    var newDiaName = _db.DialectNames.Find(Convert.ToInt32(d));
                    var newDialect = new Dialect
                    {
                        TheDialectName = newDiaName,
                        DialectName = newDiaName.DialectListName,
                        DialectListId = newDiaName.DialectListId,
                        ActorId = id

                    };
                    actor.Dialects.Add(newDialect);
                    _db.ActorDialects.Add(newDialect);
                }
            }
            //Her blir dialektene oppdatert..

            //_db.SaveChanges();

            // Lagrer fil hvis det er lastet inn en fil:
            if (uploadImg != null && uploadImg.ContentLength > 0)
            {
                actor.ImgUrl = fileOperations.SaveAndUploadImage(uploadImg, viewModel.Actor, profileImgPath);
            }

            try
            {

                _db.Entry(actor).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return RedirectToAction("Edit");
            }
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            var viewModel = new EditCreateViewModel();

            ViewBag.ActorGenderId = new SelectList(_db.Genders, "GenderId", "GenderName");
            ViewBag.DialectListId = new SelectList(_db.DialectNames, "DialectListId", "DialectListName");

            return View(viewModel);
        }

        // POST: Actor/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]

        public ActionResult CreateActor(EditCreateViewModel viewModel, HttpPostedFileBase uploadAudio, HttpPostedFileBase uploadImg, string audioTitle, string ActorGenderId)
        {


            if (ModelState.IsValid)
            {

                Actor actor = viewModel.Actor;

                var comment = actor.Comment;
                var vt = new VoiceTest();
                //string pathToSaveAudio = Server.MapPath("~/audio/");
                //string pathToSaveImg = Server.MapPath("~/img/");

                if (string.IsNullOrEmpty(ActorGenderId))
                {
                    actor.ActorGenderId = 1;
                }
                else
                {
                    actor.ActorGenderId = int.Parse(ActorGenderId);
                }

                if (uploadImg != null && uploadImg.ContentLength > 0)
                {
                    // TO-DO Sjekk for identisk navn

                    actor.ImgUrl = fileOperations.SaveAndUploadImage(uploadImg, actor, profileImgPath);
                }
                else
                {
                    actor.ImgUrl = profileImgPath + "default_profile.jpg";
                }

                if (uploadAudio != null && uploadAudio.ContentLength > 0)
                {
                    // TO-DO Sjekk for identisk navn

                    actor = fileOperations.SaveAndUploadAudio(actor, vt, comment, audioTitle, uploadAudio);
                }


                try
                {
                    _db.Actors.Add(actor);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {

                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator." + e);
                }

            }

            return RedirectToAction("Create");

        }

        [HttpGet]
        //[Route("home/franklin/{id}")]
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
        public ActionResult AddAudioFiles(int? id, string title, string comment, HttpPostedFileBase audioFile)
        {
            var actor = _db.Actors.Find(id);
            var vt = new VoiceTest();

            if (string.IsNullOrEmpty(title))
            {
                title = "Ingen tittel";
            }

            if ((audioFile.ContentType != "audio/mp3" || audioFile.ContentType != "audio/mpeg") && audioFile.ContentLength > 100000000)
            {
                ViewBag.Error = "Feil filtype eller for stor fil";
                return View(actor);
            }

            if (ModelState.IsValid)
            {

                actor = fileOperations.SaveAndUploadAudio(actor, vt, title, comment, audioFile);

            }
            //_db.SaveChanges();

            return View(actor);
        }
        [HttpGet]
        public ActionResult EditVoiceTest(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var voiceTest = _db.VoiceTests.Find(id);

            ViewBag.ActorId = new SelectList(_db.Actors, "ActorId", "FullName");
            ViewBag.ActorName = _db.Actors.Find(voiceTest.ActorId).FullName;

            return View(voiceTest);
        }

        [HttpPost]
        public ActionResult EditVoiceTest(VoiceTest voiceTest, HttpPostedFileBase audioFile, string ActorId)
        {

            var actor = _db.Actors.Find(int.Parse(ActorId));

            if (audioFile != null)
            {
                if ((audioFile.ContentType != "audio/mp3" || audioFile.ContentType != "audio/mpeg") && audioFile.ContentLength > 100000000)
                {
                    ViewBag.Error = "Feil filtype eller for stor fil";
                    return View(voiceTest);
                }
                else
                {
                    //actor = fileOperations.SaveAndUploadAudio(actor, vt, title, comment, audioFile);
                    voiceTest.VoiceTestUrl = fileOperations.SaveAndUploadVoiceTest(actor, voiceTest, audioFile);
                }
            }


            if (voiceTest.VoiceTestUrl != "false")
            {
                _db.Entry(voiceTest).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("AddAudioFiles", new { id = actor.ActorId });
            }

            return View(voiceTest);
        }


        [HttpPost]
        [Route("home/delete/{id}")]
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
                // Slette dialekter og stemmerprøver først

                var dialects = _db.ActorDialects.Where(d => d.ActorId == id.Value).ToList();
                var voiceTests = _db.VoiceTests.Where(v => v.ActorId == id.Value).ToList();

                var actor = _db.Actors.Find(id.Value);

                foreach (var dialect in dialects)
                {
                    _db.ActorDialects.Remove(dialect);
                }

                foreach (var vt in voiceTests)
                {
                    _db.VoiceTests.Remove(vt);
                }


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

        [HttpGet]
        public ActionResult DetailModal(int id)
        {
            var actor = _db.Actors.Find(id);
            return PartialView("_DetailsModal", actor);

        }

        public ActionResult AddFilesDropzone(int? id, string title)
        {
            var actor = _db.Actors.Find(id);
            var vt = new VoiceTest();

            return View(actor);
        }

        // FJERNES?
        public ActionResult SaveUploadedFile(int? id, string title, string comment)
        {
            bool isSavedSuccessfully = true;
            string fName = "";


            try
            {
                foreach (string fileName in Request.Files)
                {


                    HttpPostedFileBase file = Request.Files[fileName];

                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }
                }
            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }


    }
}
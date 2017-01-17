using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ActorLibrary.Controllers
{
    public class HomeController : Controller
    {
        private ActorContext _db = new ActorContext();

        CommonOperations comOp = new CommonOperations();
        SortingOperations sortOp = new SortingOperations();
        ActorRepository actorRepo = new ActorRepository();
        UploadFileOperations fileOperations = new UploadFileOperations();

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


        [HttpPost]
        public ActionResult Index(string sortBy)
        {

            //ViewBag.sortList = comOp.GetSortedbyList();
            //ViewBag.dialectsList = comOp.GetDialectsList();
            //ViewBag.genderList = comOp.GetGenderList();
            //ViewBag.agesList = comOp.GetAgeDefinitonsList();

            //return View(sortOp.SortBy(sortBy));
            return View();
        }

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
            var viewModel = new ActorViewModel
            {
                Actor = actor
            };

            var allGenders = _db.Genders.ToList();
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
            var viewModel = new EditCreateViewModel(id.Value);
            viewModel.Actor = actor;

            var allGenders = _db.Genders.Select(f => new SelectListItem
            {
                Value = f.GenderId.ToString(),
                Text = f.GenderName,
                Selected = f.GenderName == viewModel.Actor.ActorGenderId
            });

            //ViewBag.Dialects = actorRepo.GetDialectNames(actor);
            viewModel.GenderList = allGenders.ToList();

            return View(viewModel);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(EditCreateViewModel viewModel, HttpPostedFileBase uploadImg, string[] DialectListId)
        {
            string pathToSaveImg = Server.MapPath("~" + profileImgPath);
            var actor = viewModel.Actor;
            int id = viewModel.Actor.ActorId;
            Actor originalActor = _db.Actors.Find(id);

            if (DialectListId != null)
            {
                foreach (var d in DialectListId)
                {
                    var newDiaName = _db.DialectNames.Find(Convert.ToInt32(d));
                    var newDialect = new Dialect
                    {
                        TheDialectName = newDiaName,
                        DialectName = newDiaName.DialectListName,
                        ActorId = id

                    };
                    actor.Dialects.Add(newDialect);
                }
            }
            // Her blir dialektene oppdatert..
            foreach (var dialect in actor.Dialects)
            {
                if (!originalActor.Dialects.Contains(dialect))
                {
                    _db.ActorDialects.Add(dialect);
                }


            }


            // Lagrer fil hvis det er lastet inn en fil:
            if (uploadImg != null && uploadImg.ContentLength > 0)
            {
                actor = fileOperations.SaveAndUploadImage(uploadImg, viewModel.Actor);
            }


            try
            {


                //_db.Entry(actor).State = System.Data.Entity.EntityState.Modified;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(viewModel);
            }
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            var viewModel = new EditCreateViewModel();

            return View(viewModel);
        }

        // POST: Actor/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActor([Bind(Include = "LastName, FirstName, Address, Phone, Mail, Comment, BirthDate")]Actor actor, string ActorGenderId, string audioTitle, string comment, HttpPostedFileBase uploadAudio, HttpPostedFileBase uploadImg, string[] DialectListId)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    //string pathToSaveAudio = Server.MapPath("~/audio/");
                    //string pathToSaveImg = Server.MapPath("~/img/");
                    var vt = new VoiceTest();

                    if (uploadImg != null && uploadImg.ContentLength > 0)
                    {
                        // TO-DO Sjekk for identisk navn

                        actor = fileOperations.SaveAndUploadImage(uploadImg, actor);
                    }

                    if (uploadAudio != null && uploadAudio.ContentLength > 0)
                    {
                        // TO-DO Sjekk for identisk navn

                        actor = fileOperations.SaveAndUploadAudio(actor, vt, comment, audioTitle, uploadAudio);
                    }

                    if (string.IsNullOrEmpty(ActorGenderId))
                    {
                        //var findGender = _db.Genders.Find(Convert.ToInt32(ActorGenderId));
                        actor.ActorGenderId = "4";
                    }

                    if (DialectListId != null)
                    {
                        foreach (var d in DialectListId)
                        {
                            var newDiaName = _db.DialectNames.Find(Convert.ToInt32(d));
                            var newDialect = new Dialect
                            {
                                //TheDialectName = newDiaName
                                DialectName = newDiaName.DialectListName
                            };
                            actor.Dialects.Add(newDialect);

                        }
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
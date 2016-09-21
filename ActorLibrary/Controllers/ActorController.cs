using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActorLibrary.Models;
using System.Data.Entity.Infrastructure;
using System.IO;

namespace ActorLibrary.Controllers
{
    public class ActorController : Controller
    {
        private ActorContext _db = new ActorContext();

        // GET: Actor
        public ActionResult Index()
        {

            return View(_db.Actors.ToList());
        }

        public void PlayAudio(int? id)
        {

            RedirectToAction("Index");
        }

        // GET: Actor/Details/5
        public ActionResult Details(int id)
        {
            return View();
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





        // GET: Actor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

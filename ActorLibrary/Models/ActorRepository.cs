using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ActorLibrary.Models;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace ActorLibrary.Models
{
    public class ActorRepository
    {
        private ActorContext _db = new ActorContext();
        public string SaveUploadedImage(HttpPostedFileBase uploadImg, string pathToSaveImg, Actor actor)
        {

            // TO-DO Sjekk for identisk navn

            string filename = "profile_" + actor.Filename + "_" + actor.ActorId + ".jpg";
            string fullFilename = Path.Combine(pathToSaveImg, filename);
            uploadImg.SaveAs(fullFilename);
            //actor.ImgUrl = "/img/" + filename.ToString();
            return "/img/" + filename.ToString();

        }

        //public List<DialectName> GetDialectNames(Actor actor)
        //{

        //    var allDialects = _db.DialectNames.ToList();

        //    var dialectsList = new List<DialectName>();

        //    foreach (var dia in actor.Dialects)
        //    {
        //        dialectsList.Add(allDialects.Find(ad => ad.DialectListId == dia.DialectId));
        //    }
        //    return dialectsList;
        //}


    }
}
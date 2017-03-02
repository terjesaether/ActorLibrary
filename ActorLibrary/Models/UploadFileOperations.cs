using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;


namespace ActorLibrary.Models
{
    public class UploadFileOperations
    {
        private ActorContext _db = new ActorContext();
        private string pathToSaveAudio = HttpContext.Current.Server.MapPath("~/audio/");

        public Actor SaveAndUploadAudio(Actor actor, VoiceTest vt, string title, string comment, HttpPostedFileBase audioFile)
        {

            //string pathToSaveAudio = HttpContext.Current.Server.MapPath("~/audio/");

            if (audioFile != null & audioFile.ContentLength > 0)
            {
                // Setter filnavnet, bytter ut mellomrom og sånt
                string filename = actor.fileName().Replace(" ", "_") + "_" + title.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".mp3";
                string fullFilename = Path.Combine(pathToSaveAudio, filename);
                // Lagrer fila på disk
                audioFile.SaveAs(fullFilename);
                vt.VoiceTestTitle = title;
                vt.Comment = comment;
                vt.VoiceTestUrl = "/audio/" + filename.ToString();
                vt.ActorId = actor.ActorId;
                actor.VoiceTests.Add(vt);
                _db.VoiceTests.Add(vt);
            }

            _db.SaveChanges();

            return actor;
        }

        public string SaveAndUploadImage(HttpPostedFileBase uploadImg, Actor actor, string localSavePath)
        {

            //string localSavePath = "/Images/profiles/";
            // TO-DO Sjekk for identisk navn
            string pathToSaveImg = HttpContext.Current.Server.MapPath("~" + localSavePath);
            string filename = "profile_" + actor.Filename + "_" + actor.ActorId + ".jpg";
            string fullFilename = Path.Combine(pathToSaveImg, filename);
            uploadImg.SaveAs(fullFilename);
            //actor.ImgUrl = "/img/" + filename.ToString();
            //return "/img/" + filename.ToString();

            return localSavePath + filename.ToString();
            //return actor;

        }

        public string SaveAndUploadVoiceTest(Actor actor, VoiceTest vt, HttpPostedFileBase audioFile)
        {
            var title = vt.VoiceTestTitle;
            var comment = vt.Comment;
            string isSaved = "false";

            if (audioFile != null & audioFile.ContentLength > 0)
            {
                // Setter filnavnet, bytter ut mellomrom og sånt
                string filename = actor.fileName().Replace(" ", "_") + "_" + title.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".mp3";
                string fullFilename = Path.Combine(pathToSaveAudio, filename);

                try
                {
                    // Lagrer fila på disk
                    audioFile.SaveAs(fullFilename);
                    isSaved = "/audio/" + filename.ToString();
                }
                catch (Exception)
                {
                    isSaved = "false";
                }


                //vt.VoiceTestTitle = actor.FullName + " - " + title;
                //vt.Comment = comment;
                //vt.VoiceTestUrl = "/audio/" + filename.ToString();
                //vt.ActorId = actor.ActorId;
                //actor.VoiceTests.Add(vt);
                //_db.VoiceTests.Add(vt);
            }
            return isSaved;
        }

    }

}

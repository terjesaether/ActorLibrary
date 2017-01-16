using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActorLibrary.Models
{

    public class EditDialectViewModel
    {
        public DialectName DialectName { get; set; }
        public List<DialectName> DialectNames { get; set; }
    }

    public class EditCreateViewModel
    {
        CommonOperations comOp = new CommonOperations();
        public EditCreateViewModel()
        {
            GenderList = comOp.GetGenderList();
            DialectList = comOp.GetDialectsList();
            //Dialects = new List<DialectName>();
        }
        //private ActorContext _db = new ActorContext();

        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> DialectList { get; set; }

        //public List<DialectName> Dialects;
        public string Gender { get; set; }
        public Actor Actor { get; set; }

        //public IEnumerable<SelectListItem> GetGenderItems(string value)
        //{
        //    if (value != null)
        //    {
        //        var allGenders = _db.Genders.Select(f => new SelectListItem
        //        {
        //            Value = f.GenderId.ToString(),
        //            Text = f.GenderName,
        //            Selected = f.GenderName == value
        //        });

        //        return allGenders;
        //    }
        //    return null;
        //}

        //public List<SelectListItem> GetGenderList()
        //{
        //    var all = _db.Genders.Select(f => new SelectListItem
        //    {
        //        Value = f.GenderId.ToString(),
        //        Text = f.GenderName
        //    });
        //    return all.ToList();
        //}
        //public List<SelectListItem> GetDialectList()
        //{
        //    var all = _db.DialectNames.Select(f => new SelectListItem
        //    {
        //        Value = f.DialectListId.ToString(),
        //        Text = f.DialectListName
        //    });
        //    return all.ToList();
        //}



        //public  CheckImagePath(HttpPostedFileBase uploadImg)
        //{
        //    if (uploadImg != null && uploadImg.ContentLength > 0)
        //    {
        //        // TO-DO Sjekk for identisk navn
        //        string pathToSaveImg = Server.MapPath("~/img/");
        //        string filename = "profile" + actor.Filename + ".jpg";
        //        string fullFilename = Path.Combine(pathToSaveImg, filename);
        //        uploadImg.SaveAs(fullFilename);
        //        actor.ImgUrl = "/img/" + filename.ToString();
        //    }
        //}


    }
    public class ActorViewModel
    {
        Models.CommonOperations comOp = new Models.CommonOperations();
        public ActorViewModel()
        {

        }
        public Actor Actor { get; set; }
        public Gender Gender { get; set; }

        public List<SelectListItem> dialectList;

        //private List<SelectListItem> _ageRangeList;
        //public List<SelectListItem> ageRangeList
        //{
        //    get
        //    {
        //        return comOp.GetAgeRangesList();
        //    }

        //}



    }
}
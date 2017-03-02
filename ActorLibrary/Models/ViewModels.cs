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
        private CommonOperations comOp = new CommonOperations();
        public EditCreateViewModel()
        {
            //GenderList = comOp.GetGenderList();            

        }

        public EditCreateViewModel(int id)
        {
            GenderList = comOp.GetGenderSelectList(id);
            DialectList = comOp.GetDialectsSelectList(id);
            ActorDialects = comOp.PrintActorsDialects(id);


        }
        //private ActorContext _db = new ActorContext();

        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> DialectList { get; set; }

        public string Gender { get; set; }
        public Actor Actor { get; set; }

        public string ActorDialects;

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
        CommonOperations comOp = new CommonOperations();
        public ActorViewModel(int id)
        {
            ActorDialects = comOp.PrintActorsDialects(id);
        }
        public Actor Actor { get; set; }
        public Gender Gender { get; set; }

        public string ActorDialects;

        public IEnumerable<SelectListItem> dialectList;

        //private List<SelectListItem> _ageRangeList;
        //public List<SelectListItem> ageRangeList
        //{
        //    get
        //    {
        //        return comOp.GetAgeRangesList();
        //    }

        //}



    }

    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
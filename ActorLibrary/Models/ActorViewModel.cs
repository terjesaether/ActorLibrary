using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActorLibrary;
using System.Web.Mvc;

namespace ActorLibrary.Models
{
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
        public List<SelectListItem> ageRangeList
        {
            get
            {
                return comOp.GetAgeRangesList();
            }

        }



    }
}
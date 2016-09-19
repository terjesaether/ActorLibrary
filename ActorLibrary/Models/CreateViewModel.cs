using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActorLibrary.Models
{

    public class CreateViewModel
    {
        private ActorContext _db = new ActorContext();

        public IEnumerable<SelectListItem> GenderItems
        {
            get
            {
                var allGenders = _db.Genders.Select(f => new SelectListItem
                {
                    Value = f.GenderId.ToString(),
                    Text = f.GenderName
                });
                return allGenders;

            }
        }

        public string Gender { get; set; }

        public Actor Actor { get; set; }

    }
}
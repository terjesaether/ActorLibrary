using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActorLibrary.Models
{
    public class CommonOperations
    {
        private ActorContext _db = new ActorContext();

        public List<SelectListItem> GetAgeRangesList()
        {
            List<AgeRanges> allRanges = _db.AgeRanges.ToList();

            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in allRanges)
            {
                selectList.Add(new SelectListItem { Text = item.AgeRange, Value = item.AgeRangeId.ToString() });
            }

            return selectList;
        }

        public List<SelectListItem> GetDialectsList()
        {
            List<DialectName> allRanges = _db.DialectNames.ToList();

            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in allRanges)
            {
                selectList.Add(new SelectListItem { Text = item.DialectListName, Value = item.DialectListId.ToString() });
            }
            return selectList;
        }

        public List<SelectListItem> GetSortedbyList()
        {
            List<SelectListItem> sortByList = new List<SelectListItem>
        {
                new SelectListItem { Value = "FirstName", Text = "Fornavn"},
                new SelectListItem { Value = "LastName", Text = "Etternavn"},
                new SelectListItem { Value = "AddedDate", Text = "Lagt til dato"},
                new SelectListItem { Value = "Age", Text = "Alder"}
        };

            return sortByList;
        }

        public List<SelectListItem> GetGenderList()
        {
            var all = _db.Genders.Select(f => new SelectListItem
            {
                Value = f.GenderId.ToString(),
                Text = f.GenderName
            });
            return all.ToList();
        }
    }
}
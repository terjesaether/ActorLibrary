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


        public List<SelectListItem> GetDialectsList()
        {
            List<DialectName> all = _db.DialectNames.ToList();

            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in all)
            {
                selectList.Add(new SelectListItem { Text = item.DialectListName, Value = item.DialectListId.ToString() });
            }
            return selectList;
        }

        public List<SelectListItem> GetDialectsListWithActor(int actorId)
        {

            var actor = _db.Actors.Find(actorId);

            List<DialectName> allDialectNames = _db.DialectNames.ToList();

            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in allDialectNames)
            {
                if (!actor.Dialects.Any(d => d.DialectName == item.DialectListName))
                //.Where(a => a.Dialects.Any(d => sortByDialect.Contains(d.DialectName)))
                {
                    selectList.Add(new SelectListItem { Text = item.DialectListName, Value = item.DialectListId.ToString() });
                }

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

        public List<SelectListItem> GetAgeDefinitonsList()
        {
            List<SelectListItem> sortByList = new List<SelectListItem>
        {
                new SelectListItem { Value = "1", Text = "Barn (6-10)"},
                new SelectListItem { Value = "2", Text = "Ungdom (11-17)"},
                new SelectListItem { Value = "3", Text = "Ung voksen (18-22)"},
                new SelectListItem { Value = "4", Text = "Voksen (23-59)"},
                new SelectListItem { Value = "6", Text = "Eldre (60-90)"}
        };

            return sortByList;
        }

        public List<SelectListItem> GetGenderList()
        {

            return _db.Genders.Select(g => new SelectListItem
            {
                Value = g.GenderId.ToString(),
                Text = g.GenderName
            })
            .ToList();

        }

    }
}
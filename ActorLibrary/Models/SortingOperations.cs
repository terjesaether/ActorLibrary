using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActorLibrary.Models
{
    public class SortingOperations
    {

        private ActorContext _db = new ActorContext();

        public IEnumerable<Actor> SortActors(string sortBy, string fromAge, string toAge, string[] sortByDialect, string sortByGender, string sortByAgeDefinition)
        {
            Func<Actor, Object> orderByFunc = null;

            if (sortByDialect == null)
            {
                sortByDialect = new string[1];
                sortByDialect[0] = "0";
            }

            if (sortByGender == "")
                sortByGender = "0";

            if (sortBy == "")
                sortBy = "FirstName";

            if (sortByAgeDefinition != null || sortByAgeDefinition != "")
            {
                switch (sortByAgeDefinition)
                {
                    case "1":
                        fromAge = "6";
                        toAge = "10";
                        break;
                    case "2":
                        fromAge = "11";
                        toAge = "17";
                        break;
                    case "3":
                        fromAge = "18";
                        toAge = "22";
                        break;
                    case "4":
                        fromAge = "23";
                        toAge = "59";
                        break;
                    case "5":
                        fromAge = "60";
                        toAge = "100";
                        break;
                }
            }

            var toAgeInt = Convert.ToInt32(toAge);
            var fromAgeInt = Convert.ToInt32(fromAge);
            var sortByGenderInt = Convert.ToInt32(sortByGender);

            var range = toAgeInt - fromAgeInt;

            if (sortBy == "LastName")
                orderByFunc = item => item.LastName;
            else if (sortBy == "FirstName")
                orderByFunc = item => item.FirstName;
            else if (sortBy == "AddedDate")
                orderByFunc = item => item.AddedDate;
            else if (sortBy == "Age")
                orderByFunc = item => item.Age;


            if (sortByDialect[0] == "0" && sortByGenderInt == 1)
            {
                return SortByAgeOnly(fromAgeInt, toAgeInt, range, orderByFunc);
            }
            else if (sortByGenderInt == 1)
            {
                return SortByAgeAndDialectOnly(fromAgeInt, toAgeInt, orderByFunc, sortByDialect);
            }
            else if (sortByGenderInt > 1 && sortByDialect[0] == "0")
            {
                return SortByAgeAndGenderOnly(fromAgeInt, toAgeInt, orderByFunc, sortByGenderInt);
            }
            else
            {

                var actorQuery = _db.Actors
                .OrderByDescending(orderByFunc)
                .Where(a => a.Dialects.Any(d => sortByDialect.Contains(d.TheDialectName.DialectListId.ToString())))
                .Where(a => a.Age >= fromAgeInt)
                .Where(a => a.Age <= toAgeInt);

                if (sortByGenderInt != 0)
                {
                    actorQuery = actorQuery
                        .Where(a => a.ActorGenderId == sortByGenderInt);
                }

                return actorQuery.ToList();



                //return _db.Actors
                //.OrderByDescending(orderByFunc)
                //.Where(a => a.Dialects.Any(d => sortByDialect.Contains(d.TheDialectName.DialectListId.ToString())))
                //.Where(a => a.Age >= fromAgeInt)
                //.Where(a => a.Age <= toAgeInt)
                //.Where(a => a.ActorGenderId == sortByGenderInt.ToString())
                //.ToList();
            }

        }

        // SORTING FUNCTIONS:

        public IEnumerable<Actor> SortByAgeOnly(int fromAgeInt, int toAgeInt, int range, Func<Actor, Object> orderByFunc)
        {
            return _db.Actors
                .OrderByDescending(orderByFunc)
                .Where(a => a.Age >= fromAgeInt)
                .Where(a => a.Age <= toAgeInt)
                .ToList();
        }

        public IEnumerable<Actor> SortByAgeAndDialectOnly(int fromAgeInt, int toAgeInt, Func<Actor, Object> orderByFunc, string[] sortByDialect)
        {

            //var dialects = new List<DialectName>();
            //int id;
            //for (int i = 0; i < choosenDialects.Count(); i++)
            //{
            //    id = Convert.ToInt32(choosenDialects[i]);
            //    dialects.Add(_db.DialectNames.Find(id));
            //}

            //var sortedActors = new List<Actor>();
            //var allActors = _db.Actors
            //    .OrderByDescending(orderByFunc)
            //    .Skip(fromAgeInt)
            //    .Take(range)
            //    .Select(a => a)
            //    .ToList();


            //var allActors2 = _db.Actors
            //    .Where(a => a.Dialects.Any(d => choosenDialects.Contains(d.DialectListId.ToString())))
            //    .ToList();

            return _db.Actors
               .OrderByDescending(orderByFunc)
                .Where(a => a.Dialects.Any(d => sortByDialect.Contains(d.TheDialectName.DialectListId.ToString())))
                .Where(a => a.Age >= fromAgeInt)
                .Where(a => a.Age <= toAgeInt)
               .ToList();




            //for (int i = 0; i < allActors.Count; i++)
            //{
            //    for (int j = 0; j < dialects.Count; j++)
            //    {
            //        var newDialect = new Dialect
            //        {
            //            DialectId = dialects[j].DialectListId,
            //            TheDialectName = _db.DialectNames.Find(dialects[j].DialectListId)
            //        };

            //        for (int k = 0; k < allActors[i].Dialects.Count; k++)
            //        {
            //            if (allActors[i].Dialects[k].TheDialectName == newDialect.TheDialectName)
            //            {
            //                var a = new Actor();
            //                a = allActors[i];

            //                //a.Dialects.Add(newDialect);
            //                //actor.Dialects.Add(newDialect);
            //                sortedActors.Add(a);
            //            }
            //        }

            //        //if (actor.Dialects.Contains(newDialect))
            //        //{
            //        //    actor.Dialects.Add(newDialect);
            //        //    actors.Add(actor);
            //        //}
            //    }
            //}

            //return allActors2;

        }

        public IEnumerable<Actor> SortByAgeAndGenderOnly(int fromAgeInt, int toAgeInt, Func<Actor, Object> orderByFunc, int sortByGenderInt)
        {
            return _db.Actors
                .OrderByDescending(orderByFunc)
                .Where(a => a.Age >= fromAgeInt)
                .Where(a => a.Age <= toAgeInt)
                .Where(a => a.ActorGenderId == sortByGenderInt)
                .ToList();
        }

        public List<Actor> SortBy(string sortBy)
        {
            Func<Actor, Object> orderByFunc = null;

            if (sortBy == "LastName")
                orderByFunc = item => item.LastName;
            else if (sortBy == "FirstName")
                orderByFunc = item => item.FirstName;
            else if (sortBy == "AddedDate")
                orderByFunc = item => item.AddedDate;
            else if (sortBy == "Age")
                orderByFunc = item => item.Age;

            var sortedActors = _db.Actors
                .OrderByDescending(orderByFunc)
                .ToList();

            return sortedActors;
        }
    }


}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActorLibrary.Models
{
    public class Actor
    {
        public Actor()
        {
            VoiceTests = new List<VoiceTest>();
            Dialects = new List<Dialect>();
            AddedDate = DateTime.Now;
        }
        //[DatabaseGeneratedAttribute = ]
        [Key]
        public int ActorId { get; set; }

        [Display(Name = "Fornavn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Fødselsdato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "E-post")]
        public string Mail { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Display(Name = "Bilde")]
        public string ImgUrl { get; set; }

        [Display(Name = "Kommentar")]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "Kjønn")]
        public int ActorGenderId { get; set; }

        //public virtual Gender Gender { get; set; }

        public DateTime? AddedDate { get; set; }

        [Display(Name = "Dialekt")]
        public virtual ICollection<Dialect> Dialects { get; set; }

        //[Display(Name = "Dialekt")]
        //public virtual ICollection<DialectToActor> DialectToActors { get; set; }

        public virtual ICollection<VoiceTest> VoiceTests { get; set; }

        [Display(Name = "Fullt navn")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string fullName()
        {
            return FirstName + " " + LastName;
        }

        public string fileName()
        {
            return "_" + FirstName.ToLower() + "_" + LastName.ToLower();
        }

        public string Filename
        {
            get { return "_" + FirstName.ToLower() + "_" + LastName.ToLower(); }
        }

        [Display(Name = "Alder")]
        public int Age
        {
            get
            {
                if (BirthDate != null)
                {
                    return DateTime.Today.Year - BirthDate.Year;
                }
                return 0;
            }
        }

        //public List<string> GetDialectIds
        //{
        //    get
        //    {
        //        if (this.Dialects.Count > 0)
        //        {
        //            var dialetIdList = new List<string>();
        //            foreach (var d in Dialects)
        //            {
        //                dialetIdList.Add(d.TheDialectName.DialectListId.ToString());
        //            }
        //            return dialetIdList;
        //        }
        //        return null;

        //    }
        //}



    }

    public class VoiceTest
    {
        [Key]
        public int VoiceTestId { get; set; }
        [Display(Name = "Tittel")]
        public string VoiceTestTitle { get; set; }
        [Display(Name = "Kommentar")]
        public string Comment { get; set; }

        public string VoiceTestUrl { get; set; }
        // Lurt! Da kan man finne tilhørende Actor og lettere slette VoiceTest:
        public virtual int ActorId { get; set; }
    }

    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }

    public class AgeRanges
    {
        [Key]
        public int AgeRangeId { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public string AgeRange { get { return FromAge.ToString() + " - " + ToAge.ToString(); } }


    }
    // Listen over dialektnavnene (Trønder, Nordlending osv):




    //public class DialectToActor
    //{
    //    [Key]
    //    public int DialectToActorId { get; set; }
    //    public int ActorId { get; set; }
    //    public int DialectId { get; set; }
    //    public virtual Actor Actor { get; set; }
    //    public virtual Dialect Dialect { get; set; }
    //}





}
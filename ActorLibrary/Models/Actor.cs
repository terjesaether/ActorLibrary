using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            AddedDate = DateTime.Now;
        }

        [Key]
        public int ActorId { get; set; }

        [Display(Name = "Fornavn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Fødselsdato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
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
        public string ActorGenderId { get; set; }

        public DateTime? AddedDate { get; set; }

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

        public virtual List<VoiceTest> VoiceTests { get; set; }

        [Display(Name = "Fullt navn")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Filename
        {
            get { return "_" + FirstName.ToLower() + "_" + LastName.ToLower(); }
        }


    }

    public class VoiceTest
    {
        [Key]
        public int VoiceTestId { get; set; }
        public string VoiceTestTitle { get; set; }
        public string VoiceTestUrl { get; set; }
    }

    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }



}
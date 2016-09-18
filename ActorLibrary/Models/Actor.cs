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
        }

        [Key]
        public int ActorId { get; set; }

        [Display(Name = "Fornavn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required]
        public string LastName { get; set; }

        public string Mail { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }
        public string ImgUrl { get; set; }

        public virtual List<VoiceTest> VoiceTests { get; set; }

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

    public enum FileType
    {
        Avatar = 1, Photo
    }

}
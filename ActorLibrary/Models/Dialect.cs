using System.ComponentModel.DataAnnotations;

namespace ActorLibrary.Models
{
    // En Dialekt til Actor
    public class Dialect
    {
        [Key]
        public int DialectId { get; set; }
        public string DialectName { get; set; }
        public int ActorId { get; set; }
        public virtual DialectName TheDialectName { get; set; }
        public virtual int DialectListId { get; set; }
    }

    public class DialectName
    {
        [Key]
        public int DialectListId { get; set; }

        [Display(Name = "Dialektnavn:")]
        public string DialectListName { get; set; }
    }
}
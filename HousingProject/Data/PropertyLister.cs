using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class PropertyLister
    {
        [Key]
        public int ListerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string DoB { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherLastName { get; set; }

        [ForeignKey("listerStatus")]
        public int ListerStatus { get; set; }
        public ListerStatus listerStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<bool> IsConverted { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public ICollection<ListerDocumentsUploaded> listerDocuments { get; set; }
        public ICollection<ListerNotes> listerNotes { get; set; }
    }
}
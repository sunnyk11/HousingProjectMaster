using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class ListerDocumentsUploaded
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Lister")]
        public int ListerId { get; set; }
        public string FileName { get; set; }
        public PropertyLister Lister { get; set; }
        public DateTime UploadedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
    }
}
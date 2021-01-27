using HousingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingProject.Models
{
    public class ManagerPropertyListerViewModel
    {
        public ManagerPropertyListerViewModel()
        {
            this.PropertyLister = new PropertyLister();
            this.DocumentUpload = new List<ListerDocumentsUploaded>();
            this.Notes = new ListerNotes();
        }
        public PropertyLister PropertyLister { get; set; }
        public List<ListerDocumentsUploaded> DocumentUpload { get; set; }
        public ListerNotes Notes { get; set; }
        public string Type { get; set; }
    }
}
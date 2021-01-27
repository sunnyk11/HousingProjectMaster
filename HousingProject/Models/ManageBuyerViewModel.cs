using HousingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingProject.Models
{
    public class ManageBuyerViewModel
    {
        public ManageBuyerViewModel()
        {
            this.BuyerDetail = new BuyerDetailViewModel();
            this.FileUpload = new List<DocumentUploaded>();
            this.NoteView = new NoteViewModel();
            this.Opportunities = new List<Opportunity>();
        }
        public BuyerDetailViewModel BuyerDetail { get; set; }
        public List<DocumentUploaded> FileUpload { get; set; }
        public NoteViewModel NoteView { get; set; }
        public string Type { get; set; }
        public List<Opportunity> Opportunities { get; set; }
        
        //public Customer Customers { get; set; }
    }
}
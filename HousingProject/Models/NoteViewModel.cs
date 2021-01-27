using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingProject.Models
{
    public class NoteViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public int BuyerId { get; set; }
        public int NoteId { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingProject.Models
{
    public class BuyerDetailViewModel
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherMiddleName { get; set; }
        public int LeadStatusId { get; set; }
        public int OpportunityId { get; set; }
    }
}
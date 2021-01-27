using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HousingProject.Data;
using HousingProject.Models;

namespace HousingProject.Data
{
    public class DealerToLeadRelation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BuyerDetails")]
        public int BuyerId { get; set; }
        public string PhoneNo { get; set; }
        public string AadharCard { get; set; }
        public string PanCard { get; set; }
        public bool WillGetPayout { get; set; }
        public string BuyerName { get; set; }

        [ForeignKey("ManagerDetails")]
        public string AssignedManager { get; set; }

        [ForeignKey("AssistantManagerDetails")]
        public string AssignedAssistantManager { get; set; }

        [ForeignKey("TeamMemberDetails")]
        public string AssignedTeamMember { get; set; }
        public DateTime LeadCreatedOn { get; set; }
        public string LeadStatus { get; set; }
        public string LeadCreatedBy { get; set; }
        public string LeadVerifiedBy { get; set; }

        [ForeignKey("DealerDetails")]
        public Buyer_Detail BuyerDetails { get; set; }
        public ApplicationUser ManagerDetails { get; set; }
        public ApplicationUser AssistantManagerDetails { get; set; }
        public ApplicationUser TeamMemberDetails { get; set; }

        public ApplicationUser DealerDetails { get; set; }

        [NotMapped]
        public string LeadCreatedOnDate { get; set; }

    }
}
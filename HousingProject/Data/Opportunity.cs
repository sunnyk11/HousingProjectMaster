using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class Opportunity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        public string OpportunityName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public virtual Buyer_Detail Buyer { get; set; }

        [ForeignKey("OppoStatus")]
        public Nullable<int> OpportunityStatus { get; set; }
        public OpportunityStatus OppoStatus { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class RejectedLead
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        public string Reason { get; set; }
        public DateTime RejectedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public Buyer_Detail Buyer { get; set; }
    }
}
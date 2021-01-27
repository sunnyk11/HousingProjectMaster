using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string AadharCard { get; set; }
        public string Name { get; set; }
        public string PanCard { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public ICollection<CustomerContacts> customerContacts { get; set; }
        
        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        public virtual Buyer_Detail Buyer { get; set; }

    }
}
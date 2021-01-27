using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class CustomerContacts
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("buyers")]
        public int BuyerId { get; set; }
        public string ContactMobile { get; set; }
        public string ContactName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public Buyer_Detail buyers { get; set; }
        
    }
}
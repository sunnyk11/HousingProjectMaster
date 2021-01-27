using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class DocumentUploaded
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        public string FileName { get; set; }

        public virtual Buyer_Detail Buyer { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HousingProject.Data
{
    public class ListerStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
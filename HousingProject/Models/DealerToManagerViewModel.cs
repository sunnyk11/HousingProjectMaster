using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace HousingProject.Models
{
    public class DealerToManagerViewModel
    {
        public string DealerId { get; set; }
        public string ManagerId { get; set; }
        public string DealerName { get; set; }
        public string ManagerName { get; set; }
    }
}
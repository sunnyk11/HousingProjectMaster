using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HousingProject.Models
{
    public class NewPropertyListerViewModel
    {
        public int ListerId { get; set; }

        //[Required(ErrorMessage = "Please Select Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile No Is Required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Emaild id is Required")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
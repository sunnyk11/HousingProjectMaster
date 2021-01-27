using HousingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using HousingProject.Enum;

namespace HousingProject.Controllers
{
    public class CRMController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var opportunity = db.Opportunities.Include(x => x.Buyer).ToList();
            return View(opportunity);
        }

        public ActionResult Contacts()
        {
            var contacts = db.CustomerContacts.Include(x => x.buyers).ToList();
            return View(contacts);
        }
        
        public ActionResult Customers()
        {
            var customer = db.BuyerDetails.ToList();
            return View(customer);
        }
    }
}
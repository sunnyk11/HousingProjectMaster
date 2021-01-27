using HousingProject.Data;
using HousingProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HousingProject.Controllers
{
    [Authorize]
    public class DealerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string GetUserId()
        {
            var userId = Session["UserId"].ToString();
            return userId;
        }
        // GET: Dealer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Lead()
        {
            string userId = Session["UserId"].ToString(); ;
            ViewBag.ManagerAssigned = db.DealerToManagerRelations.Include("ManagerDetails").Where(x => x.DealerId == userId).FirstOrDefault();
            ViewBag.Message = TempData["Message"];
            var model = db.DealerLeadRelations.Include("ManagerDetails").Where(x => x.LeadCreatedBy == userId).ToList();
            foreach(var item in model)
            {
                item.LeadCreatedOnDate = String.Format("{0:MMMM}", item.LeadCreatedOn);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddLead(NewBuyerViewModel model)
        {
            var currentUser = GetUserId();
            var userDetails = db.BuyerDetails.ToList();
            bool isValid = true;

            if (model.MobileNo != null || model.Email!= null)
            {
                foreach (var item in userDetails)
                {
                    if (item.Email == model.Email)
                    {
                        isValid = false;
                    }
                    if (item.MobileNo == model.MobileNo)
                    {
                        isValid = false;
                    }
                }
            }

            if (!isValid)
            {
                var getBuyerDetails = db.BuyerDetails.Include("ManagerDetail").Where(x => x.MobileNo == model.MobileNo || x.Email == model.Email).FirstOrDefault();

                var addLeadToDealer = new DealerToLeadRelation
                {
                    BuyerId = getBuyerDetails.BuyerId,
                    PhoneNo = getBuyerDetails.MobileNo,
                    BuyerName = getBuyerDetails.BuyerName,
                    WillGetPayout = false,
                    LeadCreatedOn = DateTime.Now,
                    LeadStatus = getBuyerDetails.currenStatus,
                    LeadVerifiedBy = GetUserId()
                };
                if (getBuyerDetails.ManagerDetail != null)
                {
                    addLeadToDealer.AssignedManager = getBuyerDetails.ManagerDetail.Id;
                }

                var DealerToLeadDetails = db.DealerLeadRelations.Where(x => x.BuyerId == getBuyerDetails.BuyerId).FirstOrDefault();
                if (DealerToLeadDetails.LeadVerifiedBy != currentUser)
                {
                    db.DealerLeadRelations.Add(addLeadToDealer);
                }
                db.SaveChanges();

                TempData["Message"] = "Existing";

                return RedirectToAction("Lead");
            }

            if (ModelState.IsValid)
            {
                var currentDealer = GetUserId();
                var getManager = db.DealerToManagerRelations.Where(x => x.DealerId == currentDealer).Select(x => x.ManagerId).FirstOrDefault();

                var buyer = new Buyer_Detail
                {
                    BuyerName = model.BuyerName,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    ManagerAssigned = getManager,
                    LeadIncomingFrom = HousingProject.Enum.LeadComingFrom.Dealer.ToString(),
                    IsAlreadyCustomer = false,
                    LeadStatusId = Convert.ToInt32(Enum.LeadStatus.New),
                    LeadCreatedBy = GetUserId(),
                    CreatedOn = DateTime.Now
                };


                db.BuyerDetails.Add(buyer);
                db.SaveChanges();

                var buyerRelation = new BuyerRelation
                {
                    BuyerId = buyer.BuyerId,
                    ManagerId = getManager,
                    UpdatedOn = DateTime.Now

                };

                var addLeadToBuyer = new DealerToLeadRelation
                {
                    BuyerId = buyer.BuyerId,
                    BuyerName = buyer.BuyerName,
                    WillGetPayout = true,
                    LeadCreatedOn = DateTime.Now,
                    PhoneNo = buyer.MobileNo,
                    LeadStatus = buyer.currenStatus,
                    AssignedManager = getManager,
                    LeadCreatedBy = GetUserId()
                };

                db.BuyerRelation.Add(buyerRelation);
                db.DealerLeadRelations.Add(addLeadToBuyer);
                db.SaveChanges();
                TempData["Message"] = "New";
                return RedirectToAction("Lead");

            }
            return RedirectToAction("Index");
        }
       
    }
}
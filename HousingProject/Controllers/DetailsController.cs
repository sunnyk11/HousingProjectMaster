using HousingProject.Data;
using HousingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace HousingProject.Controllers
{
    public class DetailsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Details
        public ActionResult Index(int BuyerId)
        {
            return View();
        }
        public ActionResult ProceedLead(int id, int oppoId,string type)
        {
            var leads = db.BuyerDetails.Include(x => x.Contacts).Include(x => x.Opportunities).Include(x => x.DocumentUploadeds).Where(x => x.BuyerId == id).FirstOrDefault();
            ManageBuyerViewModel manager = new ManageBuyerViewModel();
            manager.BuyerDetail.BuyerId = leads.BuyerId;
            manager.BuyerDetail.BuyerName = leads.BuyerName;
            manager.BuyerDetail.Email = leads.Email;
            manager.BuyerDetail.MobileNo = leads.MobileNo;
            manager.BuyerDetail.DateOfBirth = leads.dob;
            manager.BuyerDetail.FatherFirstName = leads.FatherFirstName;
            manager.BuyerDetail.FatherMiddleName = leads.FatherMiddleName;
            if(oppoId == 0)
            {
                manager.Opportunities = leads.Opportunities.ToList();
            }
            else
            {
                var leadStatus = leads.Opportunities.Where(x => x.Id == oppoId).Select(x => x.OpportunityStatus).FirstOrDefault();
                manager.BuyerDetail.LeadStatusId = Convert.ToInt32(leadStatus);
                manager.BuyerDetail.OpportunityId = oppoId;
            }
            manager.FileUpload = leads.DocumentUploadeds.ToList();

            manager.Type = type;
            return View(manager);
        }

        [HttpPost]
        public JsonResult SetStatus(int BuyerId,int OpportunityId, int status)
        {
            var buyer = db.Opportunities.Where(x => x.BuyerId == BuyerId && x.Id == OpportunityId).FirstOrDefault();
            buyer.OpportunityStatus = status;
            db.SaveChanges();
            return Json(new { status = "OK", currentStatus = status });
        }

        [HttpPost]
        public JsonResult SaveDetails(BuyerDetailViewModel model)
        {
            var buyerDetail = db.BuyerDetails.Find(model.BuyerId);
            buyerDetail.FatherFirstName = model.FatherFirstName;
            buyerDetail.FatherMiddleName = model.FatherMiddleName;
            buyerDetail.Email = model.Email;
            buyerDetail.dob = model.DateOfBirth;
            buyerDetail.MobileNo = model.MobileNo;
            db.SaveChanges();
            return Json(new { status = "OK", result = model });
        }

        [HttpPost]
        public ActionResult UploadLeadFiles()
        {
            //var file = Request.Files["FileUpload"];
            int BuyerId = Convert.ToInt32(Request.Form["BuyerId"]);

            bool SavedSuccessfully = true;
            string fName = "";
            foreach (string filename in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[filename];
                var guid = Request.Form["Guid" + filename.Last()];
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Files/" + guid));
                    string pathstring = Path.Combine(path.ToString());

                    bool isexist = Directory.Exists(pathstring);
                    if (!isexist)
                    {
                        Directory.CreateDirectory(pathstring);
                    }
                    string uploadpath = string.Format("{0}\\{1}", pathstring, fName);
                    file.SaveAs(uploadpath);
                    var docDetail = new DocumentUploaded
                    {
                        BuyerId = BuyerId,
                        FileName = guid + "/" + fName,
                    };
                    db.DocumentUploadeds.Add(docDetail);
                }
            }
            db.SaveChanges();
            if (SavedSuccessfully)
            {
                return Json(new { status = "OK" });
            }
            return Json(new { status = "False" });
        }

        [HttpPost]
        public JsonResult DeleteFile(string filename)
        {
            try
            {
                var findDoc = db.DocumentUploadeds.Where(x => x.FileName == filename).FirstOrDefault();
                if (findDoc == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                db.DocumentUploadeds.Remove(findDoc);
                db.SaveChanges();
                var path = Path.Combine(Server.MapPath("~/Content/Files/"), findDoc.FileName);
                var Dirpath = Path.Combine(Server.MapPath("~/Content/Files/"), findDoc.FileName.Split('/')[0]);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    System.IO.Directory.Delete(Dirpath, true);

                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult GetContacts(int BuyerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var contacts = db.CustomerContacts.OrderBy(a => a.CreatedOn).Where(x => x.BuyerId == BuyerId).ToList();
            return Json(new { data = contacts }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveContact(int id = 0)
        {
            //var v = db.LeadNotes.Where(a => a.Id == id).FirstOrDefault();
            return View("_PartialContact", new CustomerContacts());
        }
        [HttpPost]
        public ActionResult SaveContact(CustomerContacts contact)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (contact.Id > 0)
                {
                    //Edit part is not impletementd right now
                    var v = db.CustomerContacts.Where(a => a.Id == contact.Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.ContactName = contact.ContactName;
                        v.ContactMobile = contact.ContactMobile;
                        v.ModifiedOn = contact.ModifiedOn;
                    }
                }
                else
                {
                    CustomerContacts contacts = new CustomerContacts();
                    contacts.BuyerId = contact.BuyerId;
                    contacts.ContactName = contact.ContactName;
                    contacts.ContactMobile = contact.ContactMobile;
                    contacts.CreatedOn = DateTime.Now;
                    contacts.ModifiedOn = DateTime.Now;
                    //Save
                    db.CustomerContacts.Add(contacts);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult GetOpportunity(int BuyerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var opportunities = db.Opportunities.OrderBy(a => a.CreatedOn).Where(x => x.BuyerId == BuyerId).ToList();
            return Json(new { data = opportunities }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveOpportunity(int id = 0)
        {
            //var v = db.LeadNotes.Where(a => a.Id == id).FirstOrDefault();
            return View("_PartialOpportunity", new Opportunity());
        }
        [HttpPost]
        public ActionResult SaveOpportunity(Opportunity opportunity)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (opportunity.Id > 0)
                {
                    //Edit part is not impletementd right now
                    var v = db.Opportunities.Where(a => a.Id == opportunity.Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.OpportunityName = opportunity.OpportunityName;
                        v.ModifiedOn = DateTime.Now;
                    }
                }
                else
                {
                    Opportunity oppo = new Opportunity();
                    oppo.BuyerId = opportunity.BuyerId;
                    oppo.OpportunityName = opportunity.OpportunityName;
                    oppo.OpportunityStatus = Convert.ToInt32(Enum.OpportunityStatus.CompleteApplication);
                    oppo.CreatedOn = DateTime.Now;
                    oppo.ModifiedOn = DateTime.Now;
                    //Save
                    db.Opportunities.Add(oppo);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult GetNotes(int BuyerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var notes = db.LeadNotes.OrderBy(a => a.CreatedOn).Where(x => x.BuyerId == BuyerId).ToList();
            return Json(new { data = notes }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveNote(int id = 0)
        {
            //var v = db.LeadNotes.Where(a => a.Id == id).FirstOrDefault();
            return View("_PartialNote", new LeadNotes());
        }
    
        [HttpPost]
        public ActionResult SaveNoteView(NoteViewModel note)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (note.NoteId > 0)
                {
                    //Edit 
                    var v = db.LeadNotes.Where(a => a.Id == note.NoteId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = note.Subject;
                        v.Description = note.Description;
                        v.UpdatedOn = note.ModifiedOn;
                    }
                }
                else
                {
                    LeadNotes notes = new LeadNotes();
                    notes.BuyerId = note.BuyerId;
                    notes.Subject = note.Subject;
                    notes.Description = note.Description;
                    notes.CreatedOn = DateTime.Now;
                    notes.UpdatedOn = DateTime.Now;
                    //Save
                    db.LeadNotes.Add(notes);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
using HousingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HousingProject.Data;
using System.Net;
using System.IO;
using System.Data.Entity;
using System.Threading.Tasks;
using LinqToExcel;

namespace HousingProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string GetUserId()
        {
            var userId = Session["UserId"].ToString();
            return userId;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Leads()
        {
            ViewBag.Message = TempData["Message"];
            var list = db.BuyerDetails.Include("ManagerDetail").Where(x => x.IsConverted == false || x.IsConverted == null).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult AddLeadByAdmin(NewBuyerViewModel model)
        {
            var userDetails = db.BuyerDetails.ToList();
            bool isValid = true;

            if (model.MobileNo != null || model.Email != null)
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
                TempData["Message"] = "Existing";
                return RedirectToAction("Leads");
            }

            else if (ModelState.IsValid)
            {
                var buyer = new Buyer_Detail
                {
                    BuyerName = model.BuyerName,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    IsAlreadyCustomer = false,
                    LeadStatusId = Convert.ToInt32(Enum.LeadStatus.New),
                    CreatedOn = DateTime.Now
                };

                db.BuyerDetails.Add(buyer);
                db.SaveChanges();
                TempData["Message"] = "New";
                return RedirectToAction("Leads");

            }
            return RedirectToAction("Leads");
        }
        
        public ActionResult DealerLeads()
        {
            var list = db.BuyerDetails.Include("ManagerDetail").ToList();
            return View(list);
        }
        public ActionResult Dealer()
        {
            var dealerDetail = db.Users.Where(x => x.UserType == 5).ToList();

            List<DealerToManagerViewModel> relation = new List<DealerToManagerViewModel>();

            var managerDetails = (from m in db.DealerToManagerRelations
                                  join d in db.Users on m.ManagerId equals d.Id
                                  select new
                                  {
                                      m.DealerId,
                                      m.ManagerId,
                                      m.ManagerDetails.FirstName
                                  }).ToList();
            foreach (var item in managerDetails)
            {
                relation.Add(new DealerToManagerViewModel
                {
                    DealerId = item.DealerId,
                    ManagerId = item.ManagerId,
                    ManagerName = item.FirstName
                });
            }
            ViewBag.ManagerDetails = relation;

            return View(dealerDetail);
        }
        public ActionResult Customers()
        {
            var customers = db.BuyerDetails.Where(x => x.LeadStatusId == Convert.ToInt32(Enum.LeadStatus.Converted) && x.IsConverted == true).ToList();
            return View(customers);
        }

        //[HttpPost]
        //public JsonResult AddCustomer(int Id)
        //{
        //    var leadDetails = db.BuyerDetails.Find(Id);
        //    if (leadDetails == null)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.NotFound;
        //        return Json(new { Result = "Error" });
        //    }

        //    var customer = new Customer
        //    {
        //        BuyerId = leadDetails.BuyerId,
        //        Name = leadDetails.BuyerName,
        //        MobileNo = leadDetails.MobileNo,
        //        Email = leadDetails.Email
        //    };
        //    db.Customers.Add(customer);
        //    leadDetails.IsAlreadyCustomer = true;

        //    db.SaveChanges();
        //    return Json(new { Result = "OK" });
        //}

        public ActionResult ViewCustomer()
        {
            return View();
        }

        public ActionResult ProcessLead(int id)
        {
            var leads = db.BuyerDetails.Include(x => x.DocumentUploadeds).Where(x => x.BuyerId == id).FirstOrDefault();
            ManageBuyerViewModel manager = new ManageBuyerViewModel();
            manager.BuyerDetail.BuyerId = leads.BuyerId;
            manager.BuyerDetail.BuyerName = leads.BuyerName;
            manager.BuyerDetail.Email = leads.Email;
            manager.BuyerDetail.MobileNo = leads.MobileNo;
            manager.BuyerDetail.DateOfBirth = leads.dob;
            manager.BuyerDetail.FatherFirstName = leads.FatherFirstName;
            manager.BuyerDetail.FatherMiddleName = leads.FatherMiddleName;
            manager.BuyerDetail.LeadStatusId = leads.LeadStatusId;

            manager.FileUpload = leads.DocumentUploadeds.ToList();
            return View(manager);
        }

        [HttpPost]
        public JsonResult SetStatus(int BuyerId, int status)
        {
            var buyer = db.BuyerDetails.Find(BuyerId);
            buyer.LeadStatusId = status;
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
                    var path = Path.Combine(Server.MapPath("~/Content/Files/"+guid));
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
                        FileName = guid+"/"+fName,
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
                    System.IO.Directory.Delete(Dirpath,true);
                    
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //Below code is used for upload file using different js
        //[HttpPost]
        //public ActionResult UploadFile()
        //{
        //    bool SavedSuccessfully = true;
        //    string fName = "";
        //    foreach (string filename in Request.Files)
        //    {
        //        HttpPostedFileBase file = Request.Files[filename];
        //        fName = file.FileName;
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Content/Files"));
        //            string pathstring = Path.Combine(path.ToString());
        //            string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //            bool isexist = Directory.Exists(pathstring);
        //            if (!isexist)
        //            {
        //                Directory.CreateDirectory(pathstring);
        //            }
        //            string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
        //            file.SaveAs(uploadpath);

        //        }
        //    }
        //    if (SavedSuccessfully)
        //    {
        //        return Json(new { status = "OK" });
        //    }
        //    return Json(new { status = "False" });

        //}
        public ActionResult GetNotes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var notes = db.LeadNotes.OrderBy(a => a.CreatedOn).ToList();
            return Json(new { data = notes }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveNote(int id = 0)
        {
            //var v = db.LeadNotes.Where(a => a.Id == id).FirstOrDefault();
            return View("_PartialNote",new LeadNotes());
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
        
        public ActionResult ConvertLead(int BuyerId)
        {
            var lead = db.BuyerDetails.Find(BuyerId);
            var convertLead = new ConvertLeadViewModel
            {
                BuyerId = BuyerId,
                OpportunityName = lead.BuyerName,
                ContactName = lead.BuyerName,
                AccountName = lead.BuyerName
            };
            return View(convertLead);
        }
        [HttpPost]
        public ActionResult SaveConvertedLead(ConvertLeadViewModel model)
        {
            var buyerDetail = db.BuyerDetails.Find(model.BuyerId);
            buyerDetail.BuyerName = model.AccountName;
            buyerDetail.IsConverted = true;
            
            if (ModelState.IsValid)
            {
                var opportunity = new Opportunity
                {
                    BuyerId = model.BuyerId,
                    OpportunityName = model.OpportunityName,
                    OpportunityStatus = Convert.ToInt32(Enum.OpportunityStatus.CompleteApplication),
                    CreatedOn = DateTime.Now
                };
                db.Opportunities.Add(opportunity);
                var contacts = new CustomerContacts
                {
                    CreatedOn = DateTime.Now,
                    BuyerId = model.BuyerId,
                    ContactName = model.ContactName,
                    ModifiedOn = DateTime.Now
                };
                db.CustomerContacts.Add(contacts);
                db.SaveChanges();
                return Json(new { status = "OK" });
            }
            return Json(new { status = "Error" });
        }

        public ActionResult RejectLead(int BuyerId)
        {
            RejectedLead rl = new RejectedLead();
            rl.BuyerId = BuyerId;
            return View(rl);
        }

        [HttpPost]
        public ActionResult SaveRejectedLead(RejectedLead model)
        {
            var buyerDetail = db.BuyerDetails.Find(model.BuyerId);
            buyerDetail.IsConverted = false;

            RejectedLead rlead = new RejectedLead();
            rlead.BuyerId = model.BuyerId;
            rlead.RejectedOn = DateTime.Now;
            rlead.Reason = model.Reason;
            db.RejectedLeads.Add(rlead);
            db.SaveChanges();
            return Json(new { status = "OK" });
        }
        public ActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            string data = "";
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
                    {
                        string targetpath = Server.MapPath("~/Content/Doc");
                        FileUpload.SaveAs(targetpath + filename);
                        string pathToExcelFile = targetpath + filename;

                        string sheetName = "Sheet1";

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var empDetails = from a in excelFile.Worksheet<Buyer_Detail>(sheetName) select a;
                        foreach (var a in empDetails)
                        {
                            if (a.BuyerName != null && a.MobileNo != null)
                            {

                                int resullt = PostExcelData(a.BuyerName, a.MobileNo);
                                if (resullt <= 0)
                                {
                                    data = "Hello User, Found some duplicate values! Only unique employee were inserted";
                                    ViewBag.Message = data;
                                    continue;
                                }
                                else
                                {
                                    data = "Successful uploaded records";
                                    ViewBag.Message = data;
                                }
                            }

                            else
                            {
                                ViewBag.Message = data;
                                return View("UploadExcel");
                            }
                        }
                    }

                    else
                    {
                        data = "This file is not valid format";
                        ViewBag.Message = data;
                    }
                    return View("UploadExcel");

                }
                else
                {
                    data = "Only Excel file format is allowed";
                    ViewBag.Message = data;
                    return View("UploadExcel");
                }
            }
            else
            {
                if (FileUpload == null)
                {
                    data = "Please choose Excel file";
                }

                ViewBag.Message = data;
                return View("UploadExcel");
            }
        }

        public int PostExcelData(string BuyerName, string MobileNo)
        {
            Buyer_Detail bd = new Buyer_Detail();
            int val = 0;
            var alreadyBuyer = db.BuyerDetails.Where(x => x.MobileNo == MobileNo).FirstOrDefault();
            if (alreadyBuyer == null)
            {
                bd.BuyerName = BuyerName;
                bd.MobileNo = MobileNo;
                bd.CreatedOn = DateTime.Now;
                bd.IsAlreadyCustomer = false;
                bd.LeadStatusId = Convert.ToInt32(Enum.LeadStatus.New);
                
                db.BuyerDetails.Add(bd);
                val = 1;
            }
            else
            {
                val = -1;
            }

            db.SaveChanges();
            return val;
        }
        public ActionResult TestValidation()
        {
            return View();
        }

    }
}
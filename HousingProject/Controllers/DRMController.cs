using HousingProject.Data;
using HousingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Net;

namespace HousingProject.Controllers
{
    public class DRMController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.PropertyListers.Where(x => x.IsConverted == false || x.IsConverted == null).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult AddListerByAdmin(NewPropertyListerViewModel model)
        {
            var ListerDetails = db.PropertyListers.ToList();
            bool isValid = true;

            if (model.MobileNo != null || model.Email != null)
            {
                foreach (var item in ListerDetails)
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
                return RedirectToAction("Index");
            }

            else if (ModelState.IsValid)
            {
                var lister = new PropertyLister
                {
                    Name = model.Name,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    IsConverted = false,
                    ListerStatus = Convert.ToInt32(Enum.ListerStatus.New),
                    CreatedOn = DateTime.Now
                };

                db.PropertyListers.Add(lister);
                db.SaveChanges();
                TempData["Message"] = "New";
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        public ActionResult ProcessLead(int id)
        {
            var listers = db.PropertyListers.Include(x => x.listerDocuments).Include(x => x.listerNotes).Where(x => x.ListerId == id).FirstOrDefault();
            ManagerPropertyListerViewModel models = new ManagerPropertyListerViewModel();
            models.DocumentUpload = listers.listerDocuments.ToList();
            models.PropertyLister = listers;
            return View(models);
        }

        [HttpPost]
        public JsonResult SetStatus(int ListerId, int status)
        {
            var lister = db.PropertyListers.Find(ListerId);
            lister.ListerStatus = status;
            db.SaveChanges();
            return Json(new { status = "OK", currentStatus = status });
        }

        [HttpPost]
        public JsonResult SaveDetails(PropertyLister model)
        {
            var listerDetail = db.PropertyListers.Find(model.ListerId);
            listerDetail.FatherFirstName = model.FatherFirstName;
            listerDetail.FatherLastName = model.FatherLastName;
            listerDetail.Email = model.Email;
            listerDetail.DoB = model.DoB;
            listerDetail.MobileNo = model.MobileNo;
            db.SaveChanges();
            return Json(new { status = "OK", result = model });
        }

        [HttpPost]
        public ActionResult UploadLeadFiles()
        {
            //var file = Request.Files["FileUpload"];
            int listerId = Convert.ToInt32(Request.Form["ListerId"]);

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
                    var docDetail = new ListerDocumentsUploaded
                    {
                        ListerId = listerId,
                        FileName = guid + "/" + fName,
                    };
                    db.ListerDocumentsUploadeds.Add(docDetail);
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
                var findDoc = db.ListerDocumentsUploadeds.Where(x => x.FileName == filename).FirstOrDefault();
                if (findDoc == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                db.ListerDocumentsUploadeds.Remove(findDoc);
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

        public ActionResult GetNotes(int listerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var notes = db.ListerNotes.Where(x => x.ListerId == listerId).OrderBy(a => a.CreatedOn).ToList();
            return Json(new { data = notes }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveNote(int id = 0)
        {
            //var v = db.LeadNotes.Where(a => a.Id == id).FirstOrDefault();
            return View("_PartialNote", new ListerNotes());
        }

        [HttpPost]
        public ActionResult SaveNoteView(ListerNotes note)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (note.Id > 0)
                {
                    //Edit 
                    var v = db.ListerNotes.Where(a => a.Id == note.Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = note.Subject;
                        v.Description = note.Description;
                        v.UpdatedOn = note.UpdatedOn;
                    }
                }
                else
                {
                    ListerNotes notes = new ListerNotes();
                    notes.Id = note.Id;
                    notes.Subject = note.Subject;
                    notes.Description = note.Description;
                    notes.CreatedOn = DateTime.Now;
                    notes.UpdatedOn = DateTime.Now;
                    //Save
                    db.ListerNotes.Add(notes);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
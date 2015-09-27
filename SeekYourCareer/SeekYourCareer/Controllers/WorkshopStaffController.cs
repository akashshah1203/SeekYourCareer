using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;
using System.Data.SqlClient;

namespace SeekYourCareer.Controllers
{
    [Authorize]
    public class WorkshopStaffController : Controller
    {
        //
        // GET: /WorkshopStaff/
        [HttpGet]
        public ActionResult WorkshopCandidateAll()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Admin") != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            List<string> companyNames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.WorkshopCompanies = companyNames;
            return View();
        }

        [HttpPost]
        public ActionResult SelectedWorkshopIds(string companyName)
        {
            List<int> workshopIds = new DataAccess.StaffDAL().WorkshopIdList(companyName);

            return Json(workshopIds);
        }

        [HttpPost]
        public ActionResult WorkshopApplicantDetails(int workshopId)
        {
            WSSelectedAppsVM WSApps = new DataAccess.StaffDAL().SelectedWSApplicantsD(workshopId);

            return Json(WSApps);
        }

        [HttpGet]
        public ActionResult PostWorkshop()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Admin") != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.WSCompanyNames = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult PostWorkshopDetail(string cname)
        {
            List<string> Domains = new DataAccess.StaffDAL().WSDomainsByCompany(cname);

            return Json(Domains);
        }

        [HttpPost]
        public ActionResult PostWorkshopMaster(string domain, string companyName)
        {
            WorkshopViewModel WSOffers = new DataAccess.StaffDAL().ListWorkshopsBy(domain, companyName);

            return Json(WSOffers);
        }

        [HttpGet]
        public ActionResult WorkshopApplicantAll()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Admin") != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.WSCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult WorkshopDomainList(string cname)
        {
            List<string> Domains = new DataAccess.StaffDAL().WSDomainsByCompany(cname);
            
            return Json(Domains);
        }

        [HttpPost]
        public ActionResult PendingIdList(string domain, string companyName)
        {

            List<string> WIDs = new DataAccess.StaffDAL().PendingWSByDomain(domain, companyName);
            
            return Json(WIDs);
        }

        [HttpPost]
        public ActionResult WorkshopPrerequisites(string wid)
        {
            WSPrerequisite prereq = new DataAccess.StaffDAL().WSPrerequisiteOf(wid);
            
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult WorkshopApplicantDetail(string wid)
        {
            WSPendAppViewModel WSPendApps = new DataAccess.StaffDAL().WSApplicantDetails(wid);
            
            return Json(WSPendApps);
        }

        [HttpPost]
        public ActionResult PostWorkshopOffer(int workshopId)
        {
            int returnValue = new DataAccess.StaffDAL().ApproveWorkshop(workshopId);

            return Json(returnValue);

        }

        [HttpPost]
        public ActionResult RejectWorkshopOffer(int workshopId)
        {
            int returnValue = new DataAccess.StaffDAL().RejectWorkshop(workshopId);

            return Json(returnValue);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WorkshopStaff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WorkshopStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkshopStaff/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WorkshopStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WorkshopStaff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WorkshopStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WorkshopStaff/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

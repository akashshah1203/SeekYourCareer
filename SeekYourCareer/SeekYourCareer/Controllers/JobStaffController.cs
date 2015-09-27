using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.Controllers
{
    [Authorize]
    public class JobStaffController : Controller
    {
        //
        // GET: /JobStaff/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JobCandidateAll()
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

            ViewBag.CompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult JobCandidateAll(string companyName)
        {
            List<string> JobIds = new DataAccess.StaffDAL().JobIdsByCompany(companyName);
                        
            return Json(JobIds);
        }


        [HttpPost]
        public ActionResult JobCandidateMaster(string jobId)
        {
            SelJCanViewModel jobapplns = new DataAccess.StaffDAL().SelectedAppsByJobId(jobId);
            
            return Json(jobapplns);
        }
        //
        // GET: /JobStaff/Details/5

        [HttpGet]
        public ActionResult JobApplicantAll()
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
            
            ViewBag.JobCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult JobStreamCodes(string companyName)
        {
            List<string> StreamCodes = new DataAccess.StaffDAL().StreamCodeOf(companyName);
                        
            return Json(StreamCodes);
        }

        [HttpPost]
        public ActionResult JobIdList(string streamCode)
        {
            List<string> JIDs = new DataAccess.StaffDAL().JobIdByStream(streamCode);
            
            return Json(JIDs);
        }

        [HttpPost]
        public ActionResult ViewJobPrerequisite(string jobId)
        {
            JobPrerequisite prereq = new DataAccess.StaffDAL().JobPrerequisiteOf(jobId);
            
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult JobApplicantDetail(string jobId)
        {
            JobApplicantViewModel JobApps = new DataAccess.StaffDAL().ApplicantsDetailFor(jobId);
         
            return Json(JobApps);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /JobStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /JobStaff/Create

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
        // GET: /JobStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /JobStaff/Edit/5

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
        // GET: /JobStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /JobStaff/Delete/5

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

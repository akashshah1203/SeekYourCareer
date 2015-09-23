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
    public class JStaffController : Controller
    {
        //
        // GET: /JStaff/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StSelJobCanDD()
        {
            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.CompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StSelJobCanDD(string cname)
        {
            List<string> JobIds = new DataAccess.StaffDAL().JobIdsByCompany(cname);
                        
            return Json(JobIds);
        }


        [HttpPost]
        public ActionResult StSelJobCanMD(string jobid)
        {
            SelJCanViewModel jobapplns = new DataAccess.StaffDAL().SelectedAppsByJobId(jobid);
            
            return Json(jobapplns);
        }
        //
        // GET: /JStaff/Details/5

        [HttpGet]
        public ActionResult StVwJobApp()
        {
            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();
            
            ViewBag.JobCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StVwJobApp1(string cname)
        {
            List<string> StreamCodes = new DataAccess.StaffDAL().StreamCodeOf(cname);
                        
            return Json(StreamCodes);
        }

        [HttpPost]
        public ActionResult StVwJobApp2(string streamcode)
        {
            List<string> JIDs = new DataAccess.StaffDAL().JobIdByStream(streamcode);
            
            return Json(JIDs);
        }

        [HttpPost]
        public ActionResult StVwJobPreReq(string jid)
        {
            JobPrerequisite prereq = new DataAccess.StaffDAL().JobPrerequisiteOf(jid);
            
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult StVwJobApplicants(string jid)
        {
            JobApplicantViewModel JobApps = new DataAccess.StaffDAL().ApplicantsDetailFor(jid);
         
            return Json(JobApps);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /JStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /JStaff/Create

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
        // GET: /JStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /JStaff/Edit/5

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
        // GET: /JStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /JStaff/Delete/5

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

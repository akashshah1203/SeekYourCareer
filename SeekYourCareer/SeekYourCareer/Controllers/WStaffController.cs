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
    public class WStaffController : Controller
    {
        //
        // GET: /WStaff/

        [HttpGet]
        public ActionResult StVwWSApp()
        {
            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.WSCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StVwWSApp1(string cname)
        {
            List<string> Domains = new DataAccess.StaffDAL().WSDomainsByCompany(cname);
            
            return Json(Domains);
        }

        [HttpPost]
        public ActionResult StVwWSApp2(string domain)
        {
            List<string> WIDs = new DataAccess.StaffDAL().PendingWSByDomain(domain);
            
            return Json(WIDs);
        }

        [HttpPost]
        public ActionResult StVwWSPreReq(string wid)
        {
            WSPrerequisite prereq = new DataAccess.StaffDAL().WSPrerequisiteOf(wid);
            
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult StVwWSApplicants(string wid)
        {
            WSPendAppViewModel WSPendApps = new DataAccess.StaffDAL().WSApplicantDetails(wid);
            
            return Json(WSPendApps);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WStaff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WStaff/Create

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
        // GET: /WStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WStaff/Edit/5

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
        // GET: /WStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WStaff/Delete/5

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

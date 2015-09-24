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
    public class WRepController : Controller
    {
        //
        // GET: /WRep/
        [HttpGet]
        public ActionResult SelectedWorkshopApp()
        {
            List<int> WorkshopIds = new DataAccess.RepresentativeDAL().WorkshopIdList();
            ViewBag.WorkshopIdL = WorkshopIds;
            return View();
        }

        [HttpPost]
        public ActionResult SelectedWSApplicants(int wid)
        {
            WSSelectedAppsVM WSApps = new DataAccess.RepresentativeDAL().SelectedWSApplicantsD(wid);
            
            return Json(WSApps);
        }

        [HttpGet]
        public ActionResult RVwWSApp()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Representative") != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            string RepName = (string)(Session["Username"]);
            List<string> Domains = new DataAccess.RepresentativeDAL().WorkshopDomainsAll(RepName);
            
            ViewBag.WSDomainL = Domains;
            return View();
        }

        [HttpPost]
        public ActionResult RVwWSApp1(string domain)
        {
            string repName = (string)(Session["Username"]);
            List<string> WIDs = new DataAccess.RepresentativeDAL().PendingWorkshopIDs(domain, repName);
            
            return Json(WIDs);
        }

        [HttpPost]
        public ActionResult RVwWSPreReq(string wid)
        {
            WSPrerequisite prereq = new DataAccess.RepresentativeDAL().WorkshopPrerequisiteOf(wid);
            
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult RVwWSApplicants(string wid)
        {
            string repName = (string)(Session["Username"]);
            WSPendAppViewModel WSPendApps = new DataAccess.RepresentativeDAL().PendingWSApplications(wid, repName);
        
            return Json(WSPendApps);
        }

        [HttpPost]
        public ActionResult SelectWorkshopCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().SelectWorkshopApplicant(applicantId);

            return Json(returnValue);
        }

        [HttpPost]
        public ActionResult RejectWorkshopCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().RejectWorkshopApplicant(applicantId);

            return Json(returnValue);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WRep/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WRep/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WRep/Create

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
        // GET: /WRep/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WRep/Edit/5

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
        // GET: /WRep/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WRep/Delete/5

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

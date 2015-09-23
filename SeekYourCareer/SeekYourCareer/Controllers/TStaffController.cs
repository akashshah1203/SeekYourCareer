using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekYourCareer.Models;
using System.Data.SqlClient;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.Controllers
{
    [Authorize]
    public class TStaffController : Controller
    {
        //
        // GET: /TStaff/
        [HttpGet]
        public ActionResult StVwTrainCan()
        {
            List<string> companynames = new DataAccess.StaffDAL().CompanyNames();

            ViewBag.CompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StVwTrainCanDD(string cname)
        {
            List<string> TrainIds = new DataAccess.StaffDAL().TrainingIdsByCompany(cname);
                        
            return Json(TrainIds);
        }

        [HttpPost]
        public ActionResult StVwTrainCanM(string tid)
        {
            Training traindetails = new DataAccess.StaffDAL().TrainingDetailsOfId(tid);
         
            return Json(traindetails);
        }

        [HttpPost]
        public ActionResult StVwTrainApplicants(string tid)
        {
            TrainApplicantsViewModel TrainApps = new DataAccess.StaffDAL().ApplicantDetailsForTraining(tid);
            
            return Json(TrainApps);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TStaff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TStaff/Create

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
        // GET: /TStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TStaff/Edit/5

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
        // GET: /TStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TStaff/Delete/5

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

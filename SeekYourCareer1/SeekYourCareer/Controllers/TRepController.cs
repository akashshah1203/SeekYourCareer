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
    public class TRepController : Controller
    {
        //
        // GET: /TRep/
        [HttpGet]
        public ActionResult RVwTrainCan()
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
            List<string> TrainIds = new DataAccess.RepresentativeDAL().TrainingIDListAll(RepName);

                ViewBag.TrainIdL = TrainIds;
                return View();
            
        }

        [HttpPost]
        public ActionResult RVwTrainCan(string tid)
        {
            Training traindetails = new DataAccess.RepresentativeDAL().TrainingDetailOf(tid);
            
            return Json(traindetails);
        }

        [HttpPost]
        public ActionResult RVwTrainApplicants(string tid)
        {
            TrainApplicantsViewModel TrainApps = new DataAccess.RepresentativeDAL().TrainingApplicantsFor(tid);
            
            return Json(TrainApps);
        }

        public ActionResult SelectTrainingCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().SelectTrainingApplicant(applicantId);

            return Json(returnValue);
        }

        public ActionResult RejectTrainingCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().RejectTrainingApplicant(applicantId);

            return Json(returnValue);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TRep/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TRep/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TRep/Create

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
        // GET: /TRep/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TRep/Edit/5

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
        // GET: /TRep/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TRep/Delete/5

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

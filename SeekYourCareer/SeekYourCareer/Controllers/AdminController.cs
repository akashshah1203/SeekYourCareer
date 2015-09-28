using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
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

            int work = new DataAccess.DataObj().adminTotalWorkshopApplication();
            ViewBag.totalworkshopapplication = work;
            int training = new DataAccess.DataObj().adminTotalTrainingApplication();
            ViewBag.totaltrainingapplication = training;
            int job = new DataAccess.DataObj().adminTotalJobApplication();
            ViewBag.totaljobapplication = job;
            return View();
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

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
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

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
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5
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
        //------------------------------------------------------------------------------------------------------
        //---------------------------Add new rep----------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        


        public ActionResult addNewRepresentative()
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
            return View();
        }

        [HttpPost]
        public ActionResult addNewRepresentative(AddNewRepresentative representative)
        {
            int value = new DataAccess.StaffDAL().addNewRepresent(representative);
            if (value == -1)
            {
                //successfully added
                ViewBag.message = "UnSuccessfull in adding the representative ....Please Try Again";
            }
            else
            {
                ViewBag.message = "Successfull in adding the representative .... RepresentativeId = " + value;
            }
            return View("successNewRepresentative");
        }


        //------------------------------------------------------------------------------------------------------
        //---------------------------Posting Job Offers----------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        public ActionResult adminPostJobOffer()
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
            List<string> CompanyNames = new List<string>();
            CompanyNames = new DataAccess.StaffDAL().postJobCompany();

            if (CompanyNames.Count > 0)
            {
                ViewBag.list = CompanyNames;
                return View();
            }
            else
            {
                return View("Index");
            }
            
        }
        
        [HttpPost]
        public ActionResult adminPostJobStream(string company)
        {
            List<string> stream = new List<string>();
            stream = new DataAccess.StaffDAL().postJobStream(company);

            return Json(stream);
        }

        [HttpPost]
        public ActionResult adminJobOfferTable(string company, string stream)
        {

            List<staffPostJobTable> table = new List<staffPostJobTable>();
            table = new DataAccess.StaffDAL().postJobTable(company, stream);
            return PartialView("~/Views/Admin/partialJobOfferTable.cshtml", table);
        }

        [HttpPost]
        public ActionResult adminAcceptJobOffer(string jobid, string acceptance)
        {
            bool value = new DataAccess.StaffDAL().updatePostJobOffer(jobid, acceptance);
            return Json(value);
        }



        //------------------------------------------------------------------------------------------------------
        //---------------------------Posting Training Offers----------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        public ActionResult adminPostTrainingOffer()
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
            List<string> CompanyNames = new List<string>();
            CompanyNames = new DataAccess.StaffDAL().postTrainingCompany();

            if (CompanyNames.Count > 0)
            {
                ViewBag.list = CompanyNames;
                return View();
            }
            else
            {
                return View("Index");
            }

        }

        [HttpPost]
        public ActionResult adminTrainingOfferTable(string company)
        {

            List<staffPostTrainingTable> table = new List<staffPostTrainingTable>();
            table = new DataAccess.StaffDAL().postTrainingTable(company);
            return PartialView("~/Views/Admin/adminTrainingOfferTable.cshtml", table);
        }

        [HttpPost]
        public ActionResult adminTrainingOfferDetails(string trainingID)
        {

            staffPostTrainingDetails table = new staffPostTrainingDetails();
            table = new DataAccess.StaffDAL().postTrainingDetails(trainingID);
            return PartialView("~/Views/Admin/adminTrainingOfferDetails.cshtml", table);
        }

        [HttpPost]
        public ActionResult adminAcceptTrainingOffer(string trainID, string acceptance)
        {
            bool value = new DataAccess.StaffDAL().updatePostTrainingOffer(trainID, acceptance);
            return Json(value);
        }
    }
}

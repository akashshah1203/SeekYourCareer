using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {

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

            SelectedTrainees selecttrainee = new SelectedTrainees();
            selecttrainee.CompanyModel = new List<Company>();
            selecttrainee.TrainingModel = new List<TrainingL>();
            selecttrainee.CompanyModel = GetCompanyList();
            return View(selecttrainee);
        }

        public List<Company> GetCompanyList()
        {
            List<Company> companynames = new DataAccess.StaffDAL().CompanyNamesWithRepID();
            
            return companynames;
        }

        public List<TrainingL> GetTrainings(string repid)
        {
            List<TrainingL> trainings = new DataAccess.StaffDAL().TrainingByCompanyApplied(repid);
            
            return trainings;
        }
        
        [HttpPost] 
         public ActionResult SelTrainId(string repid) 
         { 
             List<TrainingL> objtraining = new List<TrainingL>();
             objtraining = GetTrainings(repid); 
             return Json(objtraining); 
             
         } 
           
        [HttpPost]
        public ActionResult SelTrainCan(string trainid)
        {
            SelTCanViewModel trainingapplns = new DataAccess.StaffDAL().SelectedTrainingApplicants(trainid);
            
            return Json(trainingapplns);
        }

        [HttpGet]
        public ActionResult RSelTrainCan()
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
            List<string> TrainingIds = new DataAccess.RepresentativeDAL().TrainingIDListApplied(RepName);
           
            ViewBag.MyList = TrainingIds;
            return View();
        }


        [HttpPost]
        public ActionResult RSelTrainCan(string trainid)
        {
            SelTCanViewModel trainingapplns = new DataAccess.RepresentativeDAL().TrainingApplicantDetail(trainid);
            
            return Json(trainingapplns);
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Staff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Staff/Create

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
        // GET: /Staff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Staff/Edit/5

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
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Staff/Delete/5

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

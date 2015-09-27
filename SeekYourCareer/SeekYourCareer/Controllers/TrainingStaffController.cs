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
    public class TrainingStaffController : Controller
    {
        public ActionResult TrainingCandidateAll()
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

        public List<TrainingL> GetTrainings(string repId)
        {
            List<TrainingL> trainings = new DataAccess.StaffDAL().TrainingByCompanyApplied(repId);

            return trainings;
        }

        [HttpPost]
        public ActionResult TrainingIdList(string repId)
        {
            List<TrainingL> objtraining = new List<TrainingL>();
            objtraining = GetTrainings(repId);
            return Json(objtraining);

        }

        [HttpPost]
        public ActionResult TrainingCandidates(string trainingId)
        {
            SelTCanViewModel trainingapplns = new DataAccess.StaffDAL().SelectedTrainingApplicants(trainingId);

            return Json(trainingapplns);
        }

        //
        // GET: /TrainingStaff/
        [HttpGet]
        public ActionResult TrainingApplicantAll()
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
        public ActionResult TrainingApplicantAllDD(string companyName)
        {
            List<string> TrainIds = new DataAccess.StaffDAL().TrainingIdsByCompany(companyName);
                        
            return Json(TrainIds);
        }

        [HttpPost]
        public ActionResult TrainingApplicantAllM(string trainingId)
        {
            Training traindetails = new DataAccess.StaffDAL().TrainingDetailsOfId(trainingId);
         
            return Json(traindetails);
        }

        [HttpPost]
        public ActionResult ShowTrainingApplicants(string trainingId)
        {
            TrainApplicantsViewModel TrainApps = new DataAccess.StaffDAL().ApplicantDetailsForTraining(trainingId);
            
            return Json(TrainApps);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TrainingStaff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TrainingStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrainingStaff/Create

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
        // GET: /TrainingStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TrainingStaff/Edit/5

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
        // GET: /TrainingStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TrainingStaff/Delete/5

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

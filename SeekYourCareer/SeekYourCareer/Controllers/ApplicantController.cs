using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekYourCareer.Models;

namespace SeekYourCareer.Controllers
{
    public class ApplicantController : Controller
    {
        //
        // GET: /Applicant/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Applicant/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //***************************************************************************************************************************************
        //--------------------------APPLICANT TO SEARCH AND APPLY FOR JOB----------------------------------------------------------------------
        //***************************************************************************************************************************************

        public ActionResult ApplyForJob()
        {

            List<string> CompanyNames = new List<string>();
            CompanyNames = new DataAccess.ApplicantDAL().GetCompanyNames();
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
        public ActionResult ApplyForJob(string CompanyName)
        {
            List<string> streams = new List<string>();
            streams = new DataAccess.ApplicantDAL().Stream(CompanyName);

            return Json(streams);
        }
        [HttpPost]
        public ActionResult ApplyForJob1(string stream, string company)
        {
            string username = (string)Session["Username"];
            Session["UserID"] = 3;
            int Userid = (int)Session["UserID"];
            List<Job> jobdetails = new List<Job>();
            username = "akasha";
            jobdetails = new DataAccess.ApplicantDAL().JobDescription(stream, company, username, Userid);
            return Json(jobdetails);
        }


        [HttpGet]
        public ActionResult AddJobToDb(String CompanyName, String stream, String SelectedCheck, String CorrespondAddr)
        {

            int userid = (int)Session["UserID"];
            string jobid = SelectedCheck;
            DateTime AppDate = DateTime.Now;
            int appid = new DataAccess.ApplicantDAL().Addjob(userid, jobid, AppDate, CorrespondAddr);
            return RedirectToAction("ApplyForJob");
        }

        //***************************************************************************************************************************************
        //---------------------------Search and apply for training-----------------------------------------------------------------------------
        //***************************************************************************************************************************************


        //---------------------------Search for training-----------------------------------------------------------------------------
        //***************************************************************************************************************************************
        public ActionResult SearchForTraining()
        {
            //Get Domain Names
            List<string> DomainNames = new DataAccess.ApplicantDAL().GetDomainNames();
            ViewBag.List = DomainNames;
            return View();
        }

        [HttpPost]
        public ActionResult GetLocationNames(string domain)
        {
            List<string> LocationNames = new DataAccess.ApplicantDAL().GetLocation(domain);

            return Json(LocationNames);
        }

        [HttpPost]
        public ActionResult GetTrainingTable(string Domain, string Location)
        {
            List<TrainingTableData> data = new List<TrainingTableData>();
            data = new DataAccess.ApplicantDAL().GetTableData(Domain,Location);
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetDetailsTable(string TrainingID, string Company)
        {
            TrainingDetailsTable DetailList = new TrainingDetailsTable();
            DetailList = new DataAccess.ApplicantDAL().GetDetailsData(TrainingID,Company);
            return Json(DetailList);
        }
       
        
        //---------------------------Apply for training-----------------------------------------------------------------------------
        //***************************************************************************************************************************************
        [HttpGet]
        public ActionResult ApplyForTraining(string TrainingID)
        {
            String Username = (string)Session["Username"];
            ApplyForTraining app1 = new ApplyForTraining();
            app1 = new DataAccess.ApplicantDAL().ApplyDetails(Username,TrainingID);
            ViewBag.ApplyJob = app1;
            return View();
        }
        
        //***************************************************************************************************************************************



    }
}

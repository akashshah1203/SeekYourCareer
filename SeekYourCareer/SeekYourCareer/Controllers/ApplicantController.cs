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
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID=(int)Session["UserID"];
            int jobs=new DataAccess.DataObj().applicantAppliedJob(userID);
            int workshop=new DataAccess.DataObj().applicantAppliedWorkshop(userID);
            int training = new DataAccess.DataObj().applicantAppliedTraining(userID);

            ViewBag.job = Convert.ToString(jobs);
            ViewBag.works = Convert.ToString(workshop);
            ViewBag.train = Convert.ToString(training);

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
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
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
            //Session["UserID"] = 1002;
            int Userid = (int)Session["UserID"];
            List<Job> jobdetails = new List<Job>();
            //username = "akasha";
            jobdetails = new DataAccess.ApplicantDAL().JobDescription(stream, company, username, Userid);
            return Json(jobdetails);
        }


        [HttpGet]
        public ActionResult AddJobToDb(String CompanyName, String stream, String SelectedCheck, String CorrespondAddr)
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = (int)Session["UserID"];
            //userid = 1002;
            string name=(string)Session["Name"];
            string jobid = SelectedCheck;
            DateTime AppDate = DateTime.Now;
            int appid = new DataAccess.ApplicantDAL().Addjob(userid, jobid, AppDate, CorrespondAddr,name);
            return RedirectToAction("ApplyForJob");
        }

        //***************************************************************************************************************************************
        //---------------------------Search and apply for training-----------------------------------------------------------------------------
        //***************************************************************************************************************************************


        //---------------------------Search for training-----------------------------------------------------------------------------
        //***************************************************************************************************************************************
        public ActionResult SearchForTraining()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            //Get Domain Names
            //Session["UserID"] = 1002;
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
            int UserID=(int)Session["UserID"];
            List<TrainingTableData> data = new List<TrainingTableData>();
            data = new DataAccess.ApplicantDAL().GetTableData(Domain,Location,UserID);
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
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            String Username = (string)Session["Username"];
            //Username = "akasha";
            ApplyForTraining app1 = new ApplyForTraining();
            app1 = new DataAccess.ApplicantDAL().ApplyDetails(Username,TrainingID);
            ViewBag.ApplyJob = app1;
            return View();
        }

        [HttpPost]
        public ActionResult AddTrainingToDb(int userid,string trainingid,string corraddr,string corrcont)
        {
            
            string name = (string)Session["Name"];
            int ret = new DataAccess.ApplicantDAL().AddTraining(userid, trainingid, corraddr, corrcont,name);
            return Json(ret);
        }
        
        //***************************************************************************************************************************************


        //***************************************************************************************************************************************
        //---------------------------Search and apply for workshop-----------------------------------------------------------------------------
        //***************************************************************************************************************************************



        //---------------------------Search for workshop-----------------------------------------------------------------------------
        //***************************************************************************************************************************************
       
        public ActionResult WorkshopHome()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Applicant") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List<string> CompanyNames = new List<string>();
            CompanyNames = new DataAccess.ApplicantDAL().GetCompanyNamesWorkshop();
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
        public ActionResult WorkshopGetDomain(string CompanyName)
        {
            List<string> domains = new List<string>();
            domains = new DataAccess.ApplicantDAL().GetDomainWorkshop(CompanyName);

            return Json(domains);
        }

        [HttpPost]
        public ActionResult WorkshopTable(string domain,string companyname)
        {
            int userid=(int)Session["UserID"];
            
            List<WorkshopTable> table = new List<WorkshopTable>();
            table = new DataAccess.ApplicantDAL().GetTableWorkshop(domain,companyname,userid);
            
            
            return PartialView("~/Views/Applicant/WorkshopTable.cshtml",table);
        }
        [HttpPost]
        public ActionResult AddWorkshopToDb(int WorkshopID, string Location)
        {
            int userid=(int)Session["UserID"];
            bool value = new DataAccess.ApplicantDAL().AddWorkshopApplicant(WorkshopID,Location,userid);
            if (value == true)
            {
                
            }
            else
            { 
            
            }
            return RedirectToAction("Index","Applicant");
        }


    }
}

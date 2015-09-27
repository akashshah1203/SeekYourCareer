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
    public class JRepController : Controller
    {
        //
        // GET: /JRep/

        public ActionResult Index()
        {
            return View();
        }

        //The codes: RSelJobCan is to show Selected Job Candidates
        [HttpGet]
        public ActionResult RSelJobCan()
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
            List<string> JobIds = new DataAccess.RepresentativeDAL().JobIDListAll(RepName);

            ViewBag.JobIdL = JobIds;
            return View();
        }


        [HttpPost]
        public ActionResult RSelJobCan(string jobid)
        {
            SelJCanViewModel jobapplns = new DataAccess.RepresentativeDAL().SelectedJobApplicants(jobid);
         
            return Json(jobapplns);
        }
        //
        // GET: /JRep/Details/5

        //The below codes are for Viewing Job Applicants
        [HttpGet]
        public ActionResult RVwJobApp()
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
            List<string> StreamCodes = new DataAccess.RepresentativeDAL().JobStreamCodes(RepName);
            ViewBag.JobDomainL = StreamCodes;
            return View();
        }

        [HttpPost]
        public ActionResult RVwJobApp1(string streamcode)
        {
            List<string> JobIDs = new DataAccess.RepresentativeDAL().JobIDList(streamcode);

            return Json(JobIDs);
        }

        [HttpPost]
        public ActionResult RVwJobPreReq(string jobid)
        {
            JobPrerequisite prereq = new DataAccess.RepresentativeDAL().JobPrerequisiteDetail(jobid);
                    
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult RVwJobApplicants(string jid)
        {
            JobApplicantViewModel JobApps = new DataAccess.RepresentativeDAL().JobApplicantsDetail(jid);
            
            return Json(JobApps);
        }

        //.........................................................................................................................

        [HttpPost]
        public ActionResult SelectJobCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().SelectJobApplicant(applicantId);

            return Json(returnValue);

        }

        [HttpPost]
        public ActionResult RejectJobCandidate(int applicantId)
        {
            int returnValue = new DataAccess.RepresentativeDAL().RejectJobApplicant(applicantId);
            
            return Json(returnValue);
        }

    }
}

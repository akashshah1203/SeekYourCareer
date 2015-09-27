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
    public class JobRepresentativeController : Controller
    {
        //
        // GET: /JobRepresentative/

        public ActionResult Index()
        {
            return View();
        }

        //The codes: JobCandidateByCompany is to show Selected Job Candidates
        [HttpGet]
        public ActionResult JobCandidateByCompany()
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
        public ActionResult JobCandidateByCompany(string jobId)
        {
            SelJCanViewModel jobapplns = new DataAccess.RepresentativeDAL().SelectedJobApplicants(jobId);
         
            return Json(jobapplns);
        }
        //
        // GET: /JobRepresentative/Details/5

        //The below codes are for Viewing Job Applicants
        [HttpGet]
        public ActionResult JobApplicantByCompany()
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
        public ActionResult JobApplicantByCompanyList(string streamcode)
        {
            List<string> JobIDs = new DataAccess.RepresentativeDAL().JobIDList(streamcode);

            return Json(JobIDs);
        }

        [HttpPost]
        public ActionResult ViewJobPrerequisite(string jobid)
        {
            JobPrerequisite prereq = new DataAccess.RepresentativeDAL().JobPrerequisiteDetail(jobid);
                    
            return Json(prereq);
        }

        [HttpPost]
        public ActionResult JobApplicantByCompanyDetail(string jid)
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

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
            List<string> JobIds = new DataAccess.RepresentativeDAL().JobIDListAll();

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
            List<string> StreamCodes = new DataAccess.RepresentativeDAL().JobStreamCodes();
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

        public ActionResult SelectJobCandidate(string applicantid)
        {
            int n = new DataAccess.RepresentativeDAL().AddSelectedApplicant(applicantid);


            return View();
        }
     
    }
}

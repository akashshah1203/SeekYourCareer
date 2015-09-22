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
            List<string> JobIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct JobId from dbo.JobApplications;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobIds.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            ViewBag.JobIdL = JobIds;
            return View();
        }


        [HttpPost]
        public ActionResult RSelJobCan(string jobid)
        {
            SelJCanViewModel jobapplns = new SelJCanViewModel();
            jobapplns.JobApplications = new List<SelJobApplicants>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.UserID, A.JobId, A.AppDate, A.Correspondance, A.Status, B.Name, B.DOB, B.ContactNumber, B.EmailID from dbo.JobApplications A, dbo.UserDetails B WHERE A.UserID = B.UserID and A.JobId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", jobid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SelJobApplicants jobappln = new SelJobApplicants();
                    jobappln.ApplicantId = Convert.ToInt32(reader[0]);
                    jobappln.UserID = Convert.ToInt32(reader[1]);
                    jobappln.JobId = Convert.ToString(reader[2]);
                    jobappln.AppDate = Convert.ToDateTime(reader[3]);
                    jobappln.Correspondance = Convert.ToString(reader[4]);
                    jobappln.Status = Convert.ToString(reader[5]);
                    jobappln.Name = Convert.ToString(reader[6]);
                    jobappln.Age = Convert.ToDateTime(reader[7]);
                    jobappln.ContactNo = Convert.ToString(reader[8]);
                    jobappln.EmailId = Convert.ToString(reader[9]);

                    jobapplns.JobApplications.Add(jobappln);
                }
                reader.Close();
            }

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

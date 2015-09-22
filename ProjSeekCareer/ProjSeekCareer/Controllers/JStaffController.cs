using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using ProjSeekCareer.Models;
using ProjSeekCareer.ViewModels;

namespace ProjSeekCareer.Controllers
{
    public class JStaffController : Controller
    {
        //
        // GET: /JStaff/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StSelJobCanDD()
        {
            List<string> companynames = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct CompanyName from dbo.RepDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    companynames.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            ViewBag.CompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StSelJobCanDD(string cname)
        {
            List<string> JobIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.JobId from dbo.JobApplications A, dbo.JobDetails B, dbo.RepDetails C WHERE A.JobId = B.JobId AND B.RepId = C.RepID AND C.CompanyName=@filtervalue;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", cname);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobIds.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            
            return Json(JobIds);
        }


        [HttpPost]
        public ActionResult StSelJobCanMD(string jobid)
        {
            SelJCanViewModel jobapplns = new SelJCanViewModel();
            jobapplns.JobApplications = new List<SelJobApplicants>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
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
                    jobappln.ApplicantId = Convert.ToString(reader[0]);
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
        // GET: /JStaff/Details/5

        [HttpGet]
        public ActionResult StVwJobApp()
        {
            List<string> companynames = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT CompanyName from dbo.RepDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    companynames.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            ViewBag.JobCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StVwJobApp1(string cname)
        {
            List<string> StreamCodes = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.StreamCode from dbo.JobDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName=@filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", cname);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StreamCodes.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            
            return Json(StreamCodes);
        }

        [HttpPost]
        public ActionResult StVwJobApp2(string streamcode)
        {
            List<string> JIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct JobId from dbo.JobDetails WHERE StreamCode = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", streamcode);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JIDs.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return Json(JIDs);
        }

        [HttpPost]
        public ActionResult StVwJobPreReq(string jid)
        {
            JobPrerequisite prereq = new JobPrerequisite();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT MinSSCPercent, MinHSCPercent, MinGradAvg, MinPGAvg, Experience from dbo.JobDetails WHERE JobId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", jid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prereq.MinSSCPercent = Convert.ToDouble(reader[0]);
                    prereq.MinHSCPercent = Convert.ToDouble(reader[1]);
                    prereq.MinGradAvg = Convert.ToDouble(reader[2]);
                    prereq.MinPGAvg = Convert.ToDouble(reader[3]);
                    prereq.Experience = Convert.ToInt32(reader[4]);
                }
                reader.Close();
            }

            return Json(prereq);
        }

        [HttpPost]
        public ActionResult StVwJobApplicants(string jid)
        {
            JobApplicantViewModel JobApps = new JobApplicantViewModel();
            JobApps.JobApplicants = new List<JobApplicant>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.UserID, A.ApplicantId, B.Name, B.SSCPercent, B.HSCPercent, B.GradPercent, B.PGPercent, B.HaveWorkExp from dbo.JobApplications A, dbo.UserDetails B WHERE A.UserID = B.UserID AND A.JobId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", jid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobApplicant obj = new JobApplicant();
                    obj.UserID = Convert.ToInt32(reader[0]);
                    obj.ApplicantId = Convert.ToString(reader[1]);
                    obj.Name = Convert.ToString(reader[2]);
                    obj.SSCPercent = Convert.ToDouble(reader[3]);
                    obj.HSCPercent = Convert.ToInt32(reader[4]);
                    obj.GradPercent = Convert.ToDouble(reader[5]);
                    obj.PGPercent = Convert.ToDouble(reader[6]);
                    obj.HaveWorkExp = Convert.ToChar(reader[7]);

                    JobApps.JobApplicants.Add(obj);
                }
                reader.Close();
            }

            return Json(JobApps);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /JStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /JStaff/Create

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
        // GET: /JStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /JStaff/Edit/5

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
        // GET: /JStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /JStaff/Delete/5

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProjSeekCareer.Models;
using ProjSeekCareer.ViewModels;
using System.Data.SqlClient;

namespace ProjSeekCareer.Controllers
{
    public class WStaffController : Controller
    {
        //
        // GET: /WStaff/

        [HttpGet]
        public ActionResult StVwWSApp()
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
            ViewBag.WSCompanyNameL = companynames;
            return View();
        }

        [HttpPost]
        public ActionResult StVwWSApp1(string cname)
        {
            List<string> Domains = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.Domain from dbo.WorkshopDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", cname);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Domains.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return Json(Domains);
        }

        [HttpPost]
        public ActionResult StVwWSApp2(string domain)
        {
            List<string> WIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.WorkshopId from dbo.WorkshopDetails A, dbo.WorkshopAppln B WHERE A.WorkshopId = B.WorkshopId AND B.Status = 'Pending' AND A.Domain = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", domain);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WIDs.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return Json(WIDs);
        }

        [HttpPost]
        public ActionResult StVwWSPreReq(string wid)
        {
            WSPrerequisite prereq = new WSPrerequisite();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.WorkshopId, A.MinGradPct, A.MinPGPct, A.MinExperience from dbo.WorkshopDetails A WHERE A.WorkshopId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", wid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prereq.WorkshopId = Convert.ToString(reader[0]);
                    prereq.MinGradPct = Convert.ToDouble(reader[1]);
                    prereq.MinPGPct = Convert.ToDouble(reader[2]);
                    prereq.MinExperience = Convert.ToInt32(reader[3]);
                }
                reader.Close();
            }

            return Json(prereq);
        }

        [HttpPost]
        public ActionResult StVwWSApplicants(string wid)
        {
            WSPendAppViewModel WSPendApps = new WSPendAppViewModel();
            WSPendApps.PendingApps = new List<WSPendingApp>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.UserId, B.Name, B.GradPercent, B.PGPercent, B.WorkExpYears from dbo.WorkshopAppln A, dbo.UserDetails B WHERE A.UserId = B.UserID AND A.WorkshopId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", wid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WSPendingApp obj = new WSPendingApp();
                    obj.UserId = Convert.ToInt32(reader[0]);
                    obj.Name = Convert.ToString(reader[1]);
                    obj.GradPercent = Convert.ToDouble(reader[2]);
                    obj.PGPercent = Convert.ToDouble(reader[3]);
                    obj.WorkExpYears = Convert.ToInt32(reader[4]);

                    WSPendApps.PendingApps.Add(obj);
                }
                reader.Close();
            }

            return Json(WSPendApps);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WStaff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WStaff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WStaff/Create

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
        // GET: /WStaff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WStaff/Edit/5

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
        // GET: /WStaff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WStaff/Delete/5

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

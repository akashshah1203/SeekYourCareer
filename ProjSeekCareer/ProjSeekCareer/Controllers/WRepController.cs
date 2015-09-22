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
    public class WRepController : Controller
    {
        //
        // GET: /WRep/
        [HttpGet]
        public ActionResult RVwWSApp()
        {
            List<string> Domains = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct Domain from dbo.WorkshopDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Domains.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            ViewBag.WSDomainL = Domains;
            return View();
        }

        [HttpPost]
        public ActionResult RVwWSApp1(string domain)
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
        public ActionResult RVwWSPreReq(string wid)
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
        public ActionResult RVwWSApplicants(string wid)
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
        // GET: /WRep/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WRep/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WRep/Create

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
        // GET: /WRep/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WRep/Edit/5

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
        // GET: /WRep/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WRep/Delete/5

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

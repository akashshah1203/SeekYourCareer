using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;

using ProjSeekCareer.Models;
using ProjSeekCareer.ViewModels;

namespace ProjSeekCareer.Controllers
{
    public class StaffController : Controller
    {

        
        //
        // GET: /Staff/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelTrainCan2()
        {
            List<string> TrainingIds = new List<string>();
            
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct TrainingId from dbo.TrainingAppln;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainingIds.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }

            ViewBag.MyList = TrainingIds;
            return View();
        }

        public ActionResult SelTrainCan()
        {
            //TrainApplication trainingappln = new TrainApplication();
            SelTCanViewModel trainingapplns = new SelTCanViewModel();
            trainingapplns.TrainApplications = new List<TrainApplication>();
            
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                "SELECT * from dbo.TrainingAppln;";
                
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
            
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TrainApplication trainingappln = new TrainApplication();
                        trainingappln.ApplicantId = Convert.ToString(reader[0]);
                        trainingappln.UserID = Convert.ToInt32(reader[1]);
                        trainingappln.Name = Convert.ToString(reader[2]);
                        trainingappln.TrainingId = Convert.ToString(reader[3]);
                        trainingappln.AppDate = Convert.ToDateTime(reader[4]);
                        trainingappln.CorrAddress = Convert.ToString(reader[5]);
                        trainingappln.ContactNo = Convert.ToString(reader[6]);
                        trainingappln.SelectionStatus = Convert.ToString(reader[7]);
                        

                        trainingapplns.TrainApplications.Add(trainingappln);
                    }
                    reader.Close();
             }

            return View(trainingapplns);
        }

        public ActionResult SelTrainCan1(string id)
        {
            TrainApplication trainingappln = new TrainApplication();
            //string paramValue = 'AT101';

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=Bank;"
                + "Integrated Security=True";

            string queryString =
                //"SELECT * from dbo.TrainingAppln WHERE ApplicantId='@filtervalue'";
                "SELECT * from dbo.TrainingAppln WHERE ApplicantId='AT101'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@filtervalue", paramValue);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    trainingappln.ApplicantId = Convert.ToString(reader[0]);
                    trainingappln.UserID = Convert.ToInt32(reader[1]);
                    trainingappln.Name = Convert.ToString(reader[2]);
                    trainingappln.TrainingId = Convert.ToString(reader[3]);
                    trainingappln.AppDate = Convert.ToDateTime(reader[4]);
                    trainingappln.CorrAddress = Convert.ToString(reader[5]);
                    trainingappln.ContactNo = Convert.ToString(reader[6]);
                    trainingappln.SelectionStatus = Convert.ToString(reader[7]);                                        
                }
                reader.Close();
            }
            return PartialView(trainingappln);
        }
        
        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Staff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Staff/Create

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
        // GET: /Staff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Staff/Edit/5

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
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Staff/Delete/5

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

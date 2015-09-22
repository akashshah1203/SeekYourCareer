using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.Controllers
{
    public class StaffController : Controller
    {

        public ActionResult Index()
        {
            SelectedTrainees selecttrainee = new SelectedTrainees();
            selecttrainee.CompanyModel = new List<Company>();
            selecttrainee.TrainingModel = new List<TrainingL>();
            selecttrainee.CompanyModel = GetCompanyList();
            return View(selecttrainee);
        }

        public List<Company> GetCompanyList()
        {
            List<Company> companynames = new List<Company>();
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT RepId, CompanyName from dbo.RepDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Company obj = new Company();
                    obj.RepId = Convert.ToInt32(reader[0]);
                    obj.CompanyName = Convert.ToString(reader[1]);
                    companynames.Add(obj);
                }
                reader.Close();
            }
            return companynames;
        }

        public List<TrainingL> GetTrainings(string repid)
        {
            List<TrainingL> trainings = new List<TrainingL>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            //string queryString = "SELECT distinct A.TrainingId from dbo.TrainingAppln A, dbo.TrainingDetails B WHERE A.TrainingId = B.TrainingID AND B.CompanyName=@filtervalue";
            string queryString = "SELECT distinct A.TrainingId from dbo.TrainingAppln A";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", repid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainingL obj = new TrainingL();
                    obj.TrainingId = Convert.ToString(reader[0]);
                    //obj.RepId = Convert.ToString(reader[1]);
                    trainings.Add(obj);
                }
                reader.Close();
            }
            return trainings;
        }
        
        [HttpPost] 
         public ActionResult SelTrainId(string repid) 
         { 
             List<TrainingL> objtraining = new List<TrainingL>();
             objtraining = GetTrainings(repid); 
             // SelectList obj = new SelectList(objtraining, "RepId", "TrainingId"); 
             return Json(objtraining); 
             
         } 
           
        [HttpPost]
        public ActionResult SelTrainCan(string trainid)
        {
            
            SelTCanViewModel trainingapplns = new SelTCanViewModel();
            trainingapplns.TrainApplications = new List<TrainApplication>();
            
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT * from dbo.TrainingAppln WHERE TrainingId = @filtervalue;";
                
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", trainid);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TrainApplication trainingappln = new TrainApplication();
                        trainingappln.ApplicantId = Convert.ToInt32(reader[0]);
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

            return Json(trainingapplns);
        }

        [HttpGet]
        public ActionResult RSelTrainCan()
        {

            List<string> TrainingIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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


        [HttpPost]
        public ActionResult RSelTrainCan(string trainid)
        {

            SelTCanViewModel trainingapplns = new SelTCanViewModel();
            trainingapplns.TrainApplications = new List<TrainApplication>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT * from dbo.TrainingAppln WHERE TrainingId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", trainid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainApplication trainingappln = new TrainApplication();
                    trainingappln.ApplicantId = Convert.ToInt32(reader[0]);
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

            return Json(trainingapplns);
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

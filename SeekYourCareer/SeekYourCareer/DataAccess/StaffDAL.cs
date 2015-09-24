using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.DataAccess
{
    public class StaffDAL
    {
        //List of all company names used for drop down
        public List<string> CompanyNames()
        {
            List<string> companynames = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return companynames;
        }

        public List<string> StreamCodeOf(string cname)
        {
            List<string> StreamCodes = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return StreamCodes;
        }

        public List<string> JobIdByStream(string streamcode)
        {
            List<string> JIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT JobId from dbo.JobDetails WHERE StreamCode = @filtervalue;";

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
            return JIDs;
        }

        public JobPrerequisite JobPrerequisiteOf(string jid)
        {
            JobPrerequisite prereq = new JobPrerequisite();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return prereq;
        }

        public JobApplicantViewModel ApplicantsDetailFor(string jid)
        {
            JobApplicantViewModel JobApps = new JobApplicantViewModel();
            JobApps.JobApplicants = new List<JobApplicant>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.UserID, A.ApplicantId, B.Name, B.SSCPercent, B.HSCPercent, B.GradPercent, B.PGPercent, B.HaveWorkExp from dbo.JobApplications A, dbo.UserDetails B WHERE A.Status = 'Pending' AND A.UserID = B.UserID AND A.JobId = @filtervalue;";

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
                    obj.ApplicantId = Convert.ToInt32(reader[1]);
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
            return JobApps;
        }

        public List<string> TrainingIdsByCompany(string cname)
        {
            List<string> TrainIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.TrainingID from dbo.TrainingDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", cname);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainIds.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return TrainIds;
        }

        public Training TrainingDetailsOfId(string tid)
        {
            Training traindetails = new Training();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT * from dbo.TrainingDetails WHERE TrainingID = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", tid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    traindetails.TrainingID = Convert.ToString(reader[0]);
                    traindetails.RepId = Convert.ToInt32(reader[1]);
                    traindetails.Location = Convert.ToString(reader[2]);
                    traindetails.Domain = Convert.ToString(reader[3]);
                    traindetails.Graduation = Convert.ToChar(reader[4]);
                    traindetails.PG = Convert.ToChar(reader[5]);
                    traindetails.PastExp = Convert.ToInt32(reader[6]);
                    traindetails.StartingDate = Convert.ToDateTime(reader[7]);
                    traindetails.Duration = Convert.ToInt32(reader[8]);
                    traindetails.NoOfSeat = Convert.ToInt32(reader[9]);
                    traindetails.TrainingDesc = Convert.ToString(reader[10]);
                    traindetails.StaffApproval = Convert.ToString(reader[11]);
                }
                reader.Close();
            }
            return traindetails;
        }

        public TrainApplicantsViewModel ApplicantDetailsForTraining(string tid)
        {
            TrainApplicantsViewModel TrainApps = new TrainApplicantsViewModel();
            TrainApps.TrainApplicants = new List<TrainApplicant>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.Name, convert(varchar, A.AppDate), B.SSCPercent, B.HSCPercent, B.GradPercent, B.PGPercent, B.WorkExpYears from dbo.TrainingAppln A, dbo.UserDetails B WHERE A.UserID = B.UserID AND A.TrainingId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", tid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainApplicant obj = new TrainApplicant();
                    obj.ApplicantId = Convert.ToInt32(reader[0]);
                    obj.Name = Convert.ToString(reader[1]);
                    obj.AppDate = Convert.ToString(reader[2]);
                    obj.SSCPercent = Convert.ToDouble(reader[3]);
                    obj.HSCPercent = Convert.ToDouble(reader[4]);
                    obj.GradPercent = Convert.ToDouble(reader[5]);
                    obj.PGPercent = Convert.ToDouble(reader[6]);
                    obj.Experience = Convert.ToInt32(reader[7]);
                    TrainApps.TrainApplicants.Add(obj);
                }
                reader.Close();
            }
            return TrainApps;
        }

        public List<string> JobIdsByCompany(string cname)
        {
            List<string> JobIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return JobIds;
        }

        public SelJCanViewModel SelectedAppsByJobId(string jobid)
        {
            SelJCanViewModel jobapplns = new SelJCanViewModel();
            jobapplns.JobApplications = new List<SelJobApplicants>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.UserID, A.JobId, convert(varchar, A.AppDate), A.Correspondance, A.Status, B.Name, CONVERT(int, ROUND(DATEDIFF(hour,B.DOB,GETDATE())/8766.0,0)), B.ContactNumber, B.EmailID from dbo.JobApplications A, dbo.UserDetails B WHERE A.UserID = B.UserID and A.JobId = @filtervalue;";

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
                    jobappln.AppDate = Convert.ToString(reader[3]);
                    jobappln.Correspondance = Convert.ToString(reader[4]);
                    jobappln.Status = Convert.ToString(reader[5]);
                    jobappln.Name = Convert.ToString(reader[6]);
                    jobappln.Age = Convert.ToInt32(reader[7]);
                    jobappln.ContactNo = Convert.ToString(reader[8]);
                    jobappln.EmailId = Convert.ToString(reader[9]);

                    jobapplns.JobApplications.Add(jobappln);
                }
                reader.Close();
            }
            return jobapplns;
        }

        public List<string> WSDomainsByCompany(string cname)
        {
            List<string> Domains = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return Domains;
        }

        public List<string> PendingWSByDomain(string domain)
        {
            List<string> WIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.WorkshopId from dbo.WorkshopDetails A, dbo.WorkshopAppln B WHERE A.WorkshopId = B.WorkshopId AND B.Status = 'Pending' AND A.Domain = @filtervalue;";

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
            return WIDs;
        }

        public WSPrerequisite WSPrerequisiteOf(string wid)
        {
            WSPrerequisite prereq = new WSPrerequisite();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
                    prereq.WorkshopId = Convert.ToInt32(reader[0]);
                    prereq.MinGradPct = Convert.ToDouble(reader[1]);
                    prereq.MinPGPct = Convert.ToDouble(reader[2]);
                    prereq.MinExperience = Convert.ToInt32(reader[3]);
                }
                reader.Close();
            }
            return prereq;
        }

        public WSPendAppViewModel WSApplicantDetails(string wid)
        {
            WSPendAppViewModel WSPendApps = new WSPendAppViewModel();
            WSPendApps.PendingApps = new List<WSPendingApp>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
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
            return WSPendApps;
        }

        public List<Company> CompanyNamesWithRepID()
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

        public List<TrainingL> TrainingByCompanyApplied(string repid)
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

        public SelTCanViewModel SelectedTrainingApplicants(string trainid)
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
                    trainingappln.AppDate = Convert.ToString(reader[4]);
                    trainingappln.CorrAddress = Convert.ToString(reader[5]);
                    trainingappln.ContactNo = Convert.ToString(reader[6]);
                    trainingappln.SelectionStatus = Convert.ToString(reader[7]);


                    trainingapplns.TrainApplications.Add(trainingappln);
                }
                reader.Close();
            }
            return trainingapplns;
        }

        public WorkshopViewModel ListWorkshopsBy(string domain)
        {
            WorkshopViewModel wsoffers = new WorkshopViewModel();
            wsoffers.WorkshopDetail = new List<Workshop>();
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT WorkshopId, RepId, Domain, FromDate, ToDate, NoOfSeats, MinGradPct, MinPGPct, WorkshopDesc, StaffApproval, Location, MinExperience from dbo.WorkshopDetails WHERE Domain = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", domain);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Workshop wsappln = new Workshop();
                    wsappln.WorkshopId = Convert.ToInt32(reader[0]);
                    wsappln.RepId = Convert.ToInt32(reader[1]);
                    wsappln.Domain = Convert.ToString(reader[2]);
                    wsappln.FromDate = Convert.ToDateTime(reader[3]);
                    wsappln.ToDate = Convert.ToDateTime(reader[4]);
                    wsappln.NoOfSeats = Convert.ToInt32(reader[5]);
                    wsappln.MinGradPct = Convert.ToDouble(reader[6]);
                    wsappln.MinPGPct = Convert.ToDouble(reader[7]);
                    wsappln.WorkshopDesc = Convert.ToString(reader[8]);
                    wsappln.StaffApproval = Convert.ToString(reader[9]);
                    wsappln.Location = Convert.ToString(reader[10]);
                    wsappln.MinExperience = Convert.ToInt32(reader[11]);

                    wsoffers.WorkshopDetail.Add(wsappln);
                }
                reader.Close();
            }
            return wsoffers;
        }

    }
}
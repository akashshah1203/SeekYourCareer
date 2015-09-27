using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;

namespace SeekYourCareer.DataAccess
{
    public class RepresentativeDAL
    {
        public int SelectJobApplicant(int applicantid)
        {
            string connectionString ="Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"+ "Integrated Security=True";

            string queryString = "Update JobApplications SET Status='Selected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user",applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int RejectJobApplicant(int applicantid)
        {
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "Update JobApplications SET Status='Rejected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user", applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int SelectTrainingApplicant(int applicantid)
        {
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "Update TrainingAppln SET SelectionStatus='Selected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user", applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int RejectTrainingApplicant(int applicantid)
        {
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "Update TrainingAppln SET SelectionStatus='Rejected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user", applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int SelectWorkshopApplicant(int applicantid)
        {
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "Update WorkshopAppln SET Status='Selected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user", applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int RejectWorkshopApplicant(int applicantid)
        {
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "Update WorkshopAppln SET Status='Rejected' where ApplicantId=@user";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user", applicantid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public List<int> WorkshopIdList(string companyName)
        {
            List<int> WorkshopIds = new List<int>();
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.WorkshopId from dbo.WorkshopDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName=@filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", companyName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WorkshopIds.Add(Convert.ToInt32(reader[0]));
                }
                reader.Close();
            }
            return WorkshopIds;
        }

        public WSSelectedAppsVM SelectedWSApplicantsD(int wid)
        {
            WSSelectedAppsVM selwsapps = new WSSelectedAppsVM();
            selwsapps.WSSelectedApps = new List<WSSelectedApp>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.WorkshopId, convert(varchar, A.AppDate), B.Name, CONVERT(int, ROUND(DATEDIFF(hour,B.DOB,GETDATE())/8766.0,0)), B.Address, B.ContactNumber, B.EmailID from dbo.WorkshopAppln A, dbo.UserDetails B WHERE A.UserId = B.UserID and A.Status = 'Selected' and A.WorkshopId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", wid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WSSelectedApp wsappdetail = new WSSelectedApp();
                    wsappdetail.ApplicantId = Convert.ToInt32(reader[0]);
                    wsappdetail.WorkshopId = Convert.ToInt32(reader[1]);
                    wsappdetail.AppDate = Convert.ToString(reader[2]);
                    wsappdetail.Name = Convert.ToString(reader[3]);
                    wsappdetail.Age = Convert.ToInt32(reader[4]);
                    wsappdetail.Address = Convert.ToString(reader[5]);
                    wsappdetail.ContactNo = Convert.ToString(reader[6]);
                    wsappdetail.EmailId = Convert.ToString(reader[7]);

                    selwsapps.WSSelectedApps.Add(wsappdetail);
                }
                reader.Close();
            }
            return selwsapps;
        }

        //Training Applicant Details of a particular training id
        public SelTCanViewModel TrainingApplicantDetail(string trainid)
        {
            SelTCanViewModel trainingapplns = new SelTCanViewModel();
            trainingapplns.TrainApplications = new List<TrainApplication>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT ApplicantId, UserID, Name, TrainingId, convert(varchar, AppDate), CorrAddress, CorrContact, SelectionStatus from dbo.TrainingAppln WHERE TrainingId = @filtervalue;";

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

        //List all training ids for which candidates have applied
        public List<string> TrainingIDListApplied(string RepName)
        {
            List<string> TrainingIds = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.TrainingId from dbo.TrainingAppln A, dbo.TrainingDetails B WHERE A.TrainingId = B.TrainingID AND B.CompanyName=@filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", RepName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TrainingIds.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return TrainingIds;
        }

        //Details of applicants who applied for a workshop and are pending
        public WSPendAppViewModel PendingWSApplications(string wid, string repName)
        {
            WSPendAppViewModel WSPendApps = new WSPendAppViewModel();
            WSPendApps.PendingApps = new List<WSPendingApp>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.UserId, B.Name, B.GradPercent, B.PGPercent, B.WorkExpYears from dbo.WorkshopAppln A, dbo.UserDetails B, dbo.RepDetails C, dbo.WorkshopDetails D WHERE A.UserId = B.UserID AND A.Status = 'Pending' AND A.WorkshopId = D.WorkshopId AND C.RepID = D.RepId AND C.CompanyName =@companyFilter AND A.WorkshopId = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@companyFilter", repName);
                command.Parameters.AddWithValue("@filtervalue", wid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WSPendingApp obj = new WSPendingApp();
                    obj.ApplicantId = Convert.ToInt32(reader[0]);
                    obj.UserId = Convert.ToInt32(reader[1]);
                    obj.Name = Convert.ToString(reader[2]);
                    obj.GradPercent = Convert.ToDouble(reader[3]);
                    obj.PGPercent = Convert.ToDouble(reader[4]);
                    obj.WorkExpYears = Convert.ToInt32(reader[5]);

                    WSPendApps.PendingApps.Add(obj);
                }
                reader.Close();
            }
            return WSPendApps;
        }

        //Returns the workshop details of a particular workshop id
        public WSPrerequisite WorkshopPrerequisiteOf(string wid)
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

        //Returns workshop ids for which there are applications that are yet to be approved
        public List<string> PendingWorkshopIDs(string domain, string repName)
        {
            List<string> WIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.WorkshopId from dbo.WorkshopDetails A, dbo.WorkshopAppln B, dbo.RepDetails C WHERE A.WorkshopId = B.WorkshopId AND A.RepId = C.RepID AND C.CompanyName= @filter AND B.Status = 'Pending' AND A.Domain = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filter", repName);
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

        //List all workshop domains
        public List<string> WorkshopDomainsAll(string RepName)
        {
            List<string> Domains = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct Domain from dbo.WorkshopDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName=@filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", RepName);

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

        //List of Training IDs from Training Details
        public List<string> TrainingIDListAll(string companyname)
        {
            List<string> TrainIds = new List<string>();
            
            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT TrainingID from dbo.TrainingDetails WHERE CompanyName = @filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", companyname);

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

        //Return training details of a particular training id
        public Training TrainingDetailOf(string tid)
        {
            Training traindetails = new Training();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT TrainingID, RepId, Location, Domain, Graduation, PG, PastExp, StartingDate, Duration, NoOfSeat, TrainingDesc, StaffApproval from dbo.TrainingDetails WHERE TrainingID = @filtervalue;";

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

        //View details of all applicants who applied for training id: tid
        public TrainApplicantsViewModel TrainingApplicantsFor(string tid)
        {
            TrainApplicantsViewModel TrainApps = new TrainApplicantsViewModel();
            TrainApps.TrainApplicants = new List<TrainApplicant>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.Name, convert(varchar, A.AppDate), B.SSCPercent, B.HSCPercent, B.GradPercent, B.PGPercent, B.WorkExpYears from dbo.TrainingAppln A, dbo.UserDetails B WHERE A.UserID = B.UserID AND A.SelectionStatus = 'Pending' AND A.TrainingId = @filtervalue;";

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

        //List of JobIds from Job Applications table to see pending applications
        public List<string> JobIDListAll(string RepName)
        {
            List<string> JobIDs = new List<string>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT distinct A.JobId from dbo.JobApplications A, dbo.JobDetails B, dbo.RepDetails C WHERE A.JobId = B.JobId AND B.RepId = C.RepID AND C.CompanyName=@filtervalue;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", RepName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobIDs.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return JobIDs;
        }

        //To view shortlisted job applicants for a particular job id
        public SelJCanViewModel SelectedJobApplicants(string jobid)
        {
            SelJCanViewModel jobapplns = new SelJCanViewModel();
            jobapplns.JobApplications = new List<SelJobApplicants>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.ApplicantId, A.UserID, A.JobId, convert(varchar, A.AppDate), A.Correspondance, A.Status, B.Name, convert(int, ROUND(DATEDIFF(hour,B.DOB,GETDATE())/8766.0,0)), B.ContactNumber, B.EmailID from dbo.JobApplications A, dbo.UserDetails B WHERE A.UserID = B.UserID and A.Status = 'Selected' and A.JobId = @filtervalue;";

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

        //Returns List of stream codes from Job Details table - 
        public List<String> JobStreamCodes(string RepName)
        {
            List<string> StreamCodes = new List<string>();
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "SELECT distinct A.StreamCode from dbo.JobDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName=@filtervalue;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", RepName);

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

        //Returns List of Job Ids from Job Details table - 
        public List<string> JobIDList(string streamcode)
        {
            List<string> JobIDs = new List<string>();

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
                    JobIDs.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return JobIDs;
        }

        //Returns the job prerequisite details
        public JobPrerequisite JobPrerequisiteDetail(string jobid)
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
                command.Parameters.AddWithValue("@filtervalue", jobid);

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

        public JobApplicantViewModel JobApplicantsDetail(string jid)
        {
            JobApplicantViewModel JobApps = new JobApplicantViewModel();
            JobApps.JobApplicants = new List<JobApplicant>();

            string connectionString =
                "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;"
                + "Integrated Security=True";

            string queryString =
                "SELECT A.UserID, A.ApplicantId, B.Name, B.SSCPercent, B.HSCPercent, B.GradPercent, B.PGPercent, B.HaveWorkExp from dbo.JobApplications A, dbo.UserDetails B WHERE A.UserID = B.UserID AND A.Status = 'Pending' AND A.JobId = @filtervalue;";

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

    }
}
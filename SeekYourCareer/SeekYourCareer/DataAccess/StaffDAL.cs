﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SeekYourCareer.Models;
using SeekYourCareer.ViewModels;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

namespace SeekYourCareer.DataAccess
{
    public class StaffDAL
    {

        public string Connstr()
        {
            //"Data Source=(localdb)\Projects;Initial Catalog=SeekYCareer;Integrated Security=True;"
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";
            connectionString = ConfigurationManager.ConnectionStrings["ConnectToDb"].ToString();
            return connectionString;
        }

        public int ApproveWorkshop(int workshopId)
        {
            string connectionString = Connstr();

            string queryString = "Update WorkshopDetails SET StaffApproval='Approved' where WorkshopId=@offer";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@offer", workshopId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public int RejectWorkshop(int workshopId)
        {
            string connectionString = Connstr();

            string queryString = "Update WorkshopDetails SET StaffApproval='Rejected' where WorkshopId=@offer";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@offer", workshopId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return (0);
        }

        public List<int> WorkshopIdList(string companyName)
        {
            List<int> WorkshopIds = new List<int>();
            string connectionString = Connstr();

            string queryString =
                "SELECT B.WorkshopId from dbo.WorkshopDetails A, dbo.WorkshopAppln B, dbo.RepDetails C WHERE A.WorkshopId = B.WorkshopId AND A.RepId = C.RepID AND B.Status = 'Selected' AND C.CompanyName = @filtervalue;";

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

            string connectionString = Connstr();

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

        //List of all company names used for drop down
        public List<string> CompanyNames()
        {
            List<string> companynames = new List<string>();

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

            string queryString =
                "SELECT A.ApplicantId, A.UserID, A.JobId, convert(varchar, A.AppDate), A.Correspondance, A.Status, B.Name, CONVERT(int, ROUND(DATEDIFF(hour,B.DOB,GETDATE())/8766.0,0)), B.ContactNumber, B.EmailID from dbo.JobApplications A, dbo.UserDetails B WHERE A.Status = 'Selected' AND A.UserID = B.UserID and A.JobId = @filtervalue;";

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

            string connectionString = Connstr();

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

        public List<string> PendingWSByDomain(string domain, string companyName)
        {
            List<string> WIDs = new List<string>();

            string connectionString = Connstr();

            string queryString =
                "SELECT A.WorkshopId from dbo.WorkshopDetails A, dbo.WorkshopAppln B, dbo.RepDetails C WHERE A.WorkshopId = B.WorkshopId AND A.RepId = C.RepID AND B.Status = 'Pending' AND A.Domain = @filtervalue AND C.CompanyName=@filter;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", domain);
                command.Parameters.AddWithValue("@filter", companyName);

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

            string connectionString = Connstr();

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

            string connectionString = Connstr();

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
            string connectionString = Connstr();

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

            string connectionString = Connstr();

            string queryString = "SELECT distinct A.TrainingId, B.RepId from dbo.TrainingAppln A, dbo.TrainingDetails B, dbo.RepDetails C WHERE A.TrainingId = B.TrainingID AND B.RepId = C.RepID AND C.CompanyName=@filtervalue";
            

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
                    obj.RepId = Convert.ToInt32(reader[1]);
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

            string connectionString = Connstr();

            string queryString =
                "SELECT ApplicantId, UserID, Name, TrainingId, AppDate, CorrAddress, CorrContact, SelectionStatus  from dbo.TrainingAppln WHERE SelectionStatus = 'Selected' AND TrainingId = @filtervalue;";

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

        public WorkshopViewModel ListWorkshopsBy(string domain, string companyName)
        {
            WorkshopViewModel wsoffers = new WorkshopViewModel();
            wsoffers.WorkshopDetail = new List<Workshop>();
            string connectionString = Connstr();

            string queryString =
                "SELECT A.WorkshopId, A.RepId, A.Domain, A.FromDate, A.ToDate, A.NoOfSeats, A.MinGradPct, A.MinPGPct, A.WorkshopDesc, A.StaffApproval, A.Location, A.MinExperience from dbo.WorkshopDetails A, dbo.RepDetails B WHERE A.RepId = B.RepID AND B.CompanyName=@filter AND StaffApproval = 'Pending' AND Domain = @filtervalue AND A.FromDate>getdate() AND A.ToDate>getdate()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@filtervalue", domain);
                command.Parameters.AddWithValue("@filter", companyName);

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

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------Add New Representative-----------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        public int addNewRepresent(AddNewRepresentative represent)
        {

            string connectionString = Connstr();
            int repid=-1;
            string queryString = null;
            queryString = "INSERT INTO RepDetails(CompanyName,Password,Address,PhoneNumber,EmailID,Training,Workshop,Job,Internship) " +
               "values(@company,@pass,@addr,@phone,@email,@train,@workshop,@job,@intern) ;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                char train = (represent.Training == true) ? 'Y' : 'N';
                char workshop = (represent.Workshop == true) ? 'Y' : 'N';
                char job = (represent.Job == true) ? 'Y' : 'N';
                char intern = (represent.Internship == true) ? 'Y' : 'N';
                string password = GetHashedText(represent.Password);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@company",represent.companyName);
                command.Parameters.AddWithValue("@pass", password);
                command.Parameters.AddWithValue("@addr", represent.Address);
                command.Parameters.AddWithValue("@phone", represent.phoneNumber);
                command.Parameters.AddWithValue("@email", represent.EmailId);
                command.Parameters.AddWithValue("@train", train);
                command.Parameters.AddWithValue("@workshop", workshop);
                command.Parameters.AddWithValue("@job", job);
                command.Parameters.AddWithValue("@intern", intern);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                queryString = "SELECT RepID from RepDetails Where CompanyName=@name COLLATE Latin1_General_CS_AS;";
                SqlCommand command1 = new SqlCommand(queryString, connection);
                command1.Parameters.AddWithValue("@name", represent.companyName);
                connection.Open();
                repid = (int)command1.ExecuteScalar();

                connection.Close();
            }
            return repid;
        }
        //------------------------------------------------------------------------------------------------------
        //---------------------------Posting Job Offers----------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        public List<string> postJobCompany()
        {
            string connectionString = Connstr();
            List<string> companynames = new List<string>();
            string queryString = null;
            string status = "Pending";
            queryString = "SELECT distinct T1.CompanyName from RepDetails T1, JobDetails T2 Where T1.RepID=T2.RepId and T2.StaffApprovalStatus=@status and T2.AppLastDate >= GETDATE()";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@status",status);
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

        public List<string> postJobStream(string company)
        {
            string connectionString = Connstr();
            List<string> streams = new List<string>();
            string status = "Pending";
            string queryString = null;
            queryString = "SELECT distinct T2.StreamCode from dbo.RepDetails T1, dbo.JobDetails T2 Where T1.RepID=T2.RepId and T1.CompanyName=@company and " + "T2.StaffApprovalStatus=@status and T2.AppLastDate >= GETDATE()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@company", company);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    streams.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
            }
            return streams;
        
        }

        public List<staffPostJobTable> postJobTable(string company, string stream)
        {
            string connectionString = Connstr();
            List<staffPostJobTable> tableElements = new List<staffPostJobTable>();
            string queryString = null;


            queryString = "SELECT T2.JobType,T2.MinSSCPercent,T2.MinHSCPercent,T2.MinGradAvg,T2.MinPGAvg,T2.SalPerMonth,T2.Experience,T2.LocationID, " +                                        "T2.AppLastDate,T2.JobId" + 
                                " from dbo.RepDetails T1, dbo.JobDetails T2 "
                               + " Where T1.RepID=T2.RepId and T1.CompanyName=@company and T2.StreamCode=@stream and T2.StaffApprovalStatus='Pending' and "+
                               "T2.AppLastDate >= GETDATE() ";
            //Add Date to the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@company", company);
                command.Parameters.AddWithValue("@stream", stream);
                

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    staffPostJobTable table = new staffPostJobTable();
                    table.jobType = Convert.ToString(reader[0]);
                    table.MinSSCPercent = Convert.ToDecimal(reader[1]);
                    table.MinHSCPercent = Convert.ToDecimal(reader[2]);
                    table.MinGradAvg = Convert.ToDecimal(reader[3]);
                    table.MinPostAvg = Convert.ToDecimal(reader[4]);
                    table.SalPerMonth = Convert.ToInt32(reader[5]);
                    table.Experience = Convert.ToInt32(reader[6]);
                    table.LocationID = Convert.ToInt32(reader[7]);
                    table.LastDate = Convert.ToDateTime(reader[8]);
                    table.jobID = Convert.ToString(reader[9]);
                    tableElements.Add(table);
                }
                connection.Close();

                reader.Close();
            }
            return (tableElements);
        
        }

        public bool updatePostJobOffer(string jobid,string acceptance)
        {
            string connectionString = Connstr();
            string queryString = null;
            queryString = "Update JobDetails Set StaffApprovalStatus=@staff Where JobId=@jobid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@staff", acceptance);
                command.Parameters.AddWithValue("@jobid", jobid);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();

            }
            return false;
        }

        //------------------------------------------------------------------------------------------------------
        //---------------------------Posting Training Offers----------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        public List<string> postTrainingCompany()
        {
            string connectionString = Connstr();
            List<string> companynames = new List<string>();
            string queryString = null;
            string status = "Pending";
            queryString = "SELECT distinct T1.CompanyName from RepDetails T1, TrainingDetails T2 Where T1.RepID=T2.RepId and T2.StaffApproval=@status and T2.StartingDate >= GETDATE()";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@status", status);
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

       

        public List<staffPostTrainingTable> postTrainingTable(string company)
        {
            string connectionString = Connstr();
            List<staffPostTrainingTable> tableElements = new List<staffPostTrainingTable>();
            string queryString = null;


            queryString = "SELECT T2.TrainingID,T2.Location,T2.StartingDate" +
                                " from dbo.RepDetails T1, dbo.TrainingDetails T2 "
                               + " Where T1.RepID=T2.RepId and T1.CompanyName=@company and T2.StaffApproval='Pending' and " +
                               "T2.StartingDate >= GETDATE() ";
            //Add Date to the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@company", company);


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    staffPostTrainingTable table = new staffPostTrainingTable();
                    table.trainingId = Convert.ToString(reader[0]);
                    table.location = Convert.ToString(reader[1]);
                    table.startDate = Convert.ToDateTime(reader[2]);
                    tableElements.Add(table);
                }
                connection.Close();

                reader.Close();
            }
            return (tableElements);

        }

        public staffPostTrainingDetails postTrainingDetails(string trainingID)
        {
            staffPostTrainingDetails details = new staffPostTrainingDetails();
            string connectionString = Connstr();
            string queryString = null;


            queryString = "SELECT Location,Domain,Graduation,PG,PastExp,StartingDate,Duration,NoOfSeat,TrainingDesc" +
                                " from TrainingDetails  "
                               + " Where TrainingID=@train and StaffApproval='Pending' and " +
                               "StartingDate >= GETDATE() ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@train", trainingID);
                


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    details.location = Convert.ToString(reader[0]);
                    details.domain = Convert.ToString(reader[1]);
                    details.graduation = Convert.ToChar(reader[2]);
                    details.postgrad = Convert.ToChar(reader[3]);
                    details.experience = Convert.ToInt32(reader[4]);
                    details.startDate = Convert.ToDateTime(reader[5]);
                    details.duration = Convert.ToInt32(reader[6]);
                    details.seats = Convert.ToInt32(reader[7]);
                    details.description = Convert.ToString(reader[8]);

                }
                connection.Close();

                reader.Close();
            }
            return details;
        }

        public bool updatePostTrainingOffer(string trainid, string acceptance)
        {
            string connectionString = Connstr();
            string queryString = null;
            queryString = "Update TrainingDetails Set StaffApproval=@staff Where TrainingID=@train";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@staff", acceptance);
                command.Parameters.AddWithValue("@train", trainid);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();

            }
            return false;
        }
        
        //------------------------------------------------------------------------------------------------------
        //----------------------------------Hashing----------------------------------------------
        //------------------------------------------------------------------------------------------------------

        public string GetHashedText(string inputData)
        {
            byte[] tmpSource;
            byte[] tmpData;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(inputData);
            tmpData = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return Convert.ToBase64String(tmpData);
        }
    
    
    
    
    }
}
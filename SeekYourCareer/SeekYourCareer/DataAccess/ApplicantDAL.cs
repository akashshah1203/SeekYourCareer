using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekYourCareer.Models;

namespace SeekYourCareer.DataAccess
{
    public class ApplicantDAL
    {
        public string Connstr()
        {

            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";
            connectionString = ConfigurationManager.ConnectionStrings["ConnectToDb"].ToString();
            return connectionString;
        }

        public List<string> GetCompanyNames()
        {
            string connectionString = Connstr();
            List<string> companynames=new List<string>();
            string queryString = null;
            queryString = "SELECT distinct T1.CompanyName from dbo.RepDetails T1, dbo.JobDetails T2 Where T1.RepId=T2.RepId";
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

        //***************************************************************************************************************************************
        //--------------------------------WHEN APPLICANT IS APPLYING FOR JOB---------------------------------------------------------------------
        //***************************************************************************************************************************************

        public List<string> Stream(string companyname)
        {
            string connectionString = Connstr();
            List<string> streams = new List<string>();
            string queryString = null;
            queryString = "SELECT distinct T2.StreamCode from dbo.RepDetails T1, dbo.JobDetails T2 Where (T1.RepId=T2.RepId and T1.CompanyName=@company)";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@company",companyname);
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

        public List<Job> JobDescription(string stream,string company,string username,int userid)
        {
            string connectionString = Connstr();
            List<Job> job1 = new List<Job>();
            string queryString = null;
            Job j1 = new Job();
            
            String str1 = "T4.UserID=T3.UserID and T4.JobId=T2.JobId and T4.Status<>\"Applied\"";
            queryString = "SELECT T2.JobType,T2.MinSSCPercent,T2.MinHSCPercent,T2.MinGradAvg,T2.MinPGAvg,T2.SalPerMonth,T2.Experience,T2.AppLastDate,T2.JobId"                             + ",T3.Address from dbo.RepDetails T1, dbo.JobDetails T2,dbo.UserDetails T3 "
                               + " Where T1.RepID=T2.RepId and T1.CompanyName=@company and T2.StreamCode=@stream and T3.UserName=@username and T3.UserID=@userid and T3.SSCPercent>=T2.MinSSCPercent and T3.HSCPercent>=T2.MinHSCPercent and T3.GradPercent>=T2.MinGradAvg and T3.PGPercent>=T2.MinPGAvg and T3.WorkExpYears >= T2.Experience ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                command.Parameters.AddWithValue("@company", company);
                command.Parameters.AddWithValue("@stream", stream);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@userid", userid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    j1.JobType = Convert.ToString(reader[0]);
                    j1.MinSSCPercent = (float)(Convert.ToDecimal(reader[1]));
                    j1.MinHSCPercent = (float)Convert.ToDecimal(reader[2]);
                    j1.MinGradAvg = (float)Convert.ToDecimal(reader[3]);
                    j1.MinPGAvg = (float)Convert.ToDecimal(reader[4]);
                    j1.SalPerMonth = Convert.ToInt32(reader[5]);
                    j1.Experience = Convert.ToInt32(reader[6]);
                    j1.AppLastDate = Convert.ToDateTime(reader[7]);
                    j1.JobId = Convert.ToString(reader[8]);
                    j1.CorrespondanceAddress = Convert.ToString(reader[9]);
                    string queryString1 = null;
                    SqlConnection connection1 = new SqlConnection(connectionString);
                    queryString1 = "Select COUNT(*) FROM dbo.JobApplications WHERE UserID=@userid and JobId=@jobid";
                    SqlCommand command1 = new SqlCommand(queryString1, connection1);
                    
                    command1.Parameters.AddWithValue("@userid",userid);
                    command1.Parameters.AddWithValue("@jobid", j1.JobId);
                    connection1.Open();
                    SqlDataReader read1= command1.ExecuteReader();
                    read1.Read();
                    int n = Convert.ToInt32(read1[0]);
                    connection1.Close();
                    if (n == 0)
                    {
                        job1.Add(j1);
                    }
                }
                connection.Close();

                reader.Close();
            }
            return (job1);
        }

        public int Addjob(int UserID, string JobId, DateTime AppDate, string Correspondance)
        {
            int appid;
            string connectionString = Connstr();
            string queryString = null;
            queryString = "INSERT INTO dbo.JobApplications (UserID,JobId,AppDate,Correspondance,Status) " +
                "values( @userid,@jobid,@appdate,@corr,@status);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userid",UserID);
                command.Parameters.AddWithValue("@jobid", JobId);
                command.Parameters.AddWithValue("@appdate", AppDate);
                command.Parameters.AddWithValue("@corr", Correspondance);
                command.Parameters.AddWithValue("@status", "Applied");

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                queryString = "Select ApplicantId from JobApplications where UserID=@useri and JobId=@jobid1";

                SqlCommand command1 = new SqlCommand(queryString, connection);
                command1.Parameters.AddWithValue("@useri", UserID);
                command1.Parameters.AddWithValue("@jobid1", JobId);
                connection.Open();
                appid=(int)command1.ExecuteScalar();
                connection.Close();
            }
            return (appid);
        }

        //***************************************************************************************************************************************
        //--------------------------------WHEN APPLICANT IS APPLYING FOR TRAINING--------------------------------------------------------------
        //***************************************************************************************************************************************

        public List<string> GetDomainNames()
        {
            List<string> DomainName = new List<string>();

            string connectionString = Connstr();
            string queryString = null;
            queryString = "Select DISTINCT(Domain) FROM TrainingDetails";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DomainName.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
                connection.Close();
            }

            return DomainName;
        }

        public List<string> GetLocation(string domain)
        {
            List<string> LocationName = new List<string>();

            string connectionString = Connstr();
            string queryString = null;
            queryString = "Select DISTINCT(Location) FROM TrainingDetails WHERE Domain=@domain";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@domain",domain);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LocationName.Add(Convert.ToString(reader[0]));
                }
                reader.Close();
                connection.Close();
            }
            return LocationName;
        }

        public List<TrainingTableData> GetTableData(string DomainName,string Location)
        {
            List<TrainingTableData> data = new List<TrainingTableData>();
            TrainingTableData EntryIntoTable = new TrainingTableData();
            string connectionString = Connstr();
            string queryString = null;

            queryString = "Select TrainingID,TrainingDesc,RepId FROM TrainingDetails WHERE Location=@location and Domain=@domain";
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@domain", DomainName);
                command.Parameters.AddWithValue("@location", Location); 

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EntryIntoTable.TrainingID = Convert.ToString(reader[0]);
                    EntryIntoTable.Description = Convert.ToString(reader[1]);
                    
                    int RepId = Convert.ToInt32(reader[2]);
                    string queryString1 = "Select CompanyName FROM RepDetails WHERE RepID=@repid";
                    SqlConnection connection1 = new SqlConnection(connectionString);
                    SqlCommand command1 = new SqlCommand(queryString1, connection1);
                    command1.Parameters.AddWithValue("@repid",RepId);
                    
                    connection1.Open();
                    string CompanyName =(string)command1.ExecuteScalar();
                    connection1.Close();
                    
                    EntryIntoTable.Company = CompanyName;

                    data.Add(EntryIntoTable);

                }
                reader.Close();
                connection.Close();
            }
            return data;
        }

        public TrainingDetailsTable GetDetailsData(string trainingid, string company)
        {
            TrainingDetailsTable DetailList = new TrainingDetailsTable();

            string connectionString = Connstr();
            string queryString = null;
            queryString = "Select Address,EmailID,PhoneNumber FROM RepDetails WHERE CompanyName=@comapny";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@company",company);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                DetailList.CompanyName = company;
                DetailList.Address = Convert.ToString(reader[0]);
                DetailList.EmailID = Convert.ToString(reader[1]);
                DetailList.Phone = Convert.ToString(reader[2]);
                reader.Close();
                connection.Close();

                queryString = "Select StartingDate,Duration,Graduation,PG,PastExp,NoOfSeat FROM TrainingDetails WHERE TrainingID=@trainingid";
                SqlCommand command1 = new SqlCommand(queryString, connection);
                command1.Parameters.AddWithValue("@trainingid", trainingid);
                connection.Open();
                SqlDataReader reader1 = command1.ExecuteReader();
                reader1.Read();
                DetailList.StartDate = Convert.ToDateTime(reader1[0]);
                DetailList.Duration = Convert.ToInt32(reader1[1]);
                DetailList.Graduation = Convert.ToChar(reader1[2]);
                DetailList.PostGraduation = Convert.ToChar(reader1[3]);
                DetailList.PastExp = Convert.ToInt32(reader1[4]);
                DetailList.SeatsAvailable = Convert.ToInt32(reader1[5]);
                reader1.Close();
                connection.Close();
            }
            return DetailList;
        }
    }
}
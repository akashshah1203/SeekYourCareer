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

        public List<Job> JobDescription(string stream,string company,string username)
        {
            string connectionString = Connstr();
            List<Job> job1 = new List<Job>();
            string queryString = null;
            Job j1 = new Job();
            queryString = "SELECT T2.JobType,T2.MinSSCPercent,T2.MinHSCPercent,T2.MinGradAvg,T2.MinPGAvg,T2.SalPerMonth,T2.Experience,T2.AppLastDate " 
                               + "from dbo.RepDetails T1, dbo.JobDetails T2,dbo.UserDetails T3 " 
                               + "Where T1.RepId=T2.RepId and T1.CompanyName=@company and T2.StreamCode=@stream and T3.UserName=@username and T3.SSCPercent>=T2.MinSSCPercent and T3.HSCPercent>=T2.MinHSCPercent and T3.GradPercent>=T2.MinGradAvg and T3.PGPercent>=T2.MinPGAvg";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@company", company);
                command.Parameters.AddWithValue("@stream", stream);
                command.Parameters.AddWithValue("@username", username);

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

                    job1.Add(j1);
                }

                reader.Close();
            }
            return (job1);
        }
    }
}
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
        public int AddSelectedApplicant(string applicantid)
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

        //Returns List of stream codes from Job Details table - 
        public List<String> JobStreamCodes()
        {
            List<string> StreamCodes = new List<string>();
            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

            string queryString = "SELECT distinct StreamCode from dbo.JobDetails;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

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
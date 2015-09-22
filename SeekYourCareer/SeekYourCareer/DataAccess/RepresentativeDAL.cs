using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    }
}
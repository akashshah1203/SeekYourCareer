using SeekYourCareer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SeekYourCareer.DataAccess
{
    public class DataObj
    {

        public string Connstr()
        {

            string connectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";
            connectionString = ConfigurationManager.ConnectionStrings["ConnectToDb"].ToString();
            return connectionString;
        }

        public int InsertUser(Applicant model)
        {
            string connectionString = Connstr();
            SqlConnection connection = new SqlConnection(connectionString);

            String username = model.UserName;
            String password = model.Password;
            String name = model.Name;
            DateTime dob = model.DateOfBirth;
            String Contact = model.ContactNumber;
            String Email = model.EmailId;
            String address = model.Address;
            Double sscp = model.SscPercentage;
            Double hscp = model.HscPercentage;
            Char gradcomp = (model.CompletedGraduation == true) ? '1' : '0';
            Double Gradperc = model.GraduationPercentage;
            Char Pgradcomp = (model.CompletedPostGraduation == true) ? '1' : '0';
            Double PGradperc = model.PostGraduationPercentage;
            Char havework = (model.PreviousWorkExperience == true) ? '1' : '0';
            Double workexp = model.WorkExperience;

            string queryString = "INSERT INTO dbo.UserDetails (UserName,Password,Name,DOB,ContactNumber,EmailID,Address,SSCPercent,HSCPercent,GradComplete,GradPercent,PGComplete,PGPercent,HaveWorkExp,WorkExpYears) "+
                "values( @username,@password,@name,@dob,@Contact,@Email,@address,@sscp,@hscp,@gradcomp,@Gradperc,@Pgradcomp,@PGradperc,@havework,@workexp);" ;
            
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@username",username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@dob", dob);
            command.Parameters.AddWithValue("@Contact", Contact);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@sscp", sscp);
            command.Parameters.AddWithValue("@hscp", hscp);
            command.Parameters.AddWithValue("@gradcomp", gradcomp);
            command.Parameters.AddWithValue("@Gradperc", Gradperc);
            command.Parameters.AddWithValue("@Pgradcomp", Pgradcomp);
            command.Parameters.AddWithValue("@PGradperc", PGradperc);
            command.Parameters.AddWithValue("@havework", havework);
            command.Parameters.AddWithValue("@workexp", workexp);
            command.ExecuteNonQuery();
            connection.Close();
            queryString = "SELECT UserID from dbo.UserDetails Where UserName=@username ;";
            SqlCommand command1 = new SqlCommand(queryString, connection);
            connection.Open();
            int userid = (int)command1.ExecuteScalar();
           
            connection.Close();
            return userid;
        }


        public bool validateUser(string Username,string Password,string Type)
        {


            string connectionString = Connstr();
            int num=0;

            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = null;
            connection.Open();
            if (Type.CompareTo("Admin") == 0)
                {
                    //queryString = "Select count(UserName) from UserDetails where UserName=@usern and Password=@Pass";
                    String adminUser, adminpass;
                    adminUser = ConfigurationManager.AppSettings["Username"];
                    adminpass = ConfigurationManager.AppSettings["Password"];
                    if (Username.CompareTo(adminUser) == 0 && Password.CompareTo(adminpass) == 0)
                    {
                        
                        return true;
                    }
                    return false;
                }
                if (Type.CompareTo("Applicant") == 0)
                {
                    queryString = "Select count(UserName) from UserDetails where UserName=@usern and Password=@Pass";
                    

                } 
                if (Type.CompareTo("Representative") == 0)
                {
                    queryString = "Select count(CompanyName) from RepDetails where CompanyName=@usern and Password=@Pass";
                }
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@usern", Username);
                command.Parameters.AddWithValue("@Pass", Password);
                num = (int)command.ExecuteScalar();
                connection.Close();
            if (num == 1)
                return true; 




            return false;
        }

        public bool ChangePassword(string username,string oldP,string newP)
        {
            string connectionString = Connstr();


            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = null;
            connection.Open();
            queryString = "Select count(UserName) from UserDetails where UserName=@usern and Password=@Pass";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@usern", username);
            command.Parameters.AddWithValue("@Pass", oldP);
            int num = (int)command.ExecuteScalar();

            if (num == 1)
            {
                queryString = "Update UserDetails Set Password=@newP Where Username=@username";
                command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@newP", oldP);
                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }


}
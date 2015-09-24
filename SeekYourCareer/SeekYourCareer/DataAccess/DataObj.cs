using SeekYourCareer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

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
            password = GetHashedText(password);
            String name = model.Name;
            DateTime dob = model.DateOfBirth;
            String Contact = model.ContactNumber;
            String Email = model.EmailId;
            String address = model.Address;
            Double sscp = model.SscPercentage;
            Double hscp = model.HscPercentage;
            Char gradcomp = (model.CompletedGraduation == true) ? 'Y' : 'N';
            Double Gradperc = model.GraduationPercentage  ;
            Char Pgradcomp = (model.CompletedPostGraduation == true) ? 'Y' : 'N';
            Double PGradperc = model.PostGraduationPercentage;
            Char havework = (model.PreviousWorkExperience == true) ? 'Y' : 'N';
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
            queryString = "SELECT UserID from dbo.UserDetails Where UserName=@username COLLATE Latin1_General_CS_AS;";
            SqlCommand command1 = new SqlCommand(queryString, connection);
            command1.Parameters.AddWithValue("@username",username);
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
                    Password = GetHashedText(Password);

                    queryString = "Select count(UserName) from UserDetails where UserName=@usern and Password=@Pass COLLATE Latin1_General_CS_AS";
                    

                } 
                if (Type.CompareTo("Representative") == 0)
                {
                    Password = GetHashedText(Password);

                    queryString = "Select count(CompanyName) from RepDetails where CompanyName=@usern and Password=@Pass COLLATE Latin1_General_CS_AS";
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

        public int GetUserID(string Username)
        {
            int id=0;
            string connectionString = Connstr();
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = null;
            queryString = "Select UserID from UserDetails where UserName=@usern COLLATE Latin1_General_CS_AS";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@usern", Username);
            connection.Open();
            id = (int)command.ExecuteScalar();
            connection.Close();

            connection.Open();

            return id;
        }
       
        public string GetName(string Username)
        {
            string name;
            string connectionString = Connstr();
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = null;
            queryString = "Select Name from UserDetails where UserName=@usern COLLATE Latin1_General_CS_AS";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@usern", Username);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            name = Convert.ToString(reader[0]);

            
            reader.Close();
            connection.Close();

            connection.Open();

            return name;
        }
        
        public bool ChangePassword(string username,string oldPassword,string newPassword,string usertype)
        {
            string connectionString = Connstr();


            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = null;
            connection.Open();
            if (usertype.CompareTo("Applicant") == 0)
                queryString = "Select count(UserName) from UserDetails where UserName=@usern and Password=@Pass COLLATE Latin1_General_CS_AS";
            else if (usertype.CompareTo("Representative") == 0)
                queryString = "Select count(RepID) from RepDetails where CompanyName=@usern and Password=@Pass COLLATE Latin1_General_CS_AS";
            else
                return false;
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@usern", username);
            command.Parameters.AddWithValue("@Pass", oldPassword);
            int num = (int)command.ExecuteScalar();
            connection.Close();

            if (num == 1)
            {
                if (usertype.CompareTo("Applicant") == 0)
                    queryString = "Update UserDetails Set Password=@newP Where Username=@username";
                else if (usertype.CompareTo("Representative") == 0)
                    queryString = "Update RepDetails Set Password=@newP Where CompanyName=@username";
                SqlConnection newconnection = new SqlConnection(connectionString);
                SqlCommand newcommand = new SqlCommand(queryString, newconnection);
                newcommand.Parameters.AddWithValue("@username", username);
                newcommand.Parameters.AddWithValue("@newP", newPassword);
                newconnection.Open();
                newcommand.ExecuteNonQuery();
                newconnection.Close();
                return true;
            }
            return false;
        }


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
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace SeekCareerWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 
    {
        public static string ConnectionString = "Data Source=(localdb)\\Projects;Initial Catalog=SeekYCareer;" + "Integrated Security=True";

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "checkavailability?username={userName}")]
        public string CheckAvailability(string userName)
        {
            try
            {
                using (SqlConnection sqlDBConnection = new SqlConnection(ConnectionString))
                {
                    StringBuilder sqlstmt = new StringBuilder("select count(*) from UserDetails where UserName = @userName COLLATE Latin1_General_CS_AS ");
                    //sqlstmt.Append(Convert.ToString(userid));
                    SqlCommand myCommand = new SqlCommand(sqlstmt.ToString(), sqlDBConnection);
                    myCommand.CommandType = CommandType.Text;
                    myCommand.Parameters.AddWithValue("@userName", userName);

                    bool foundRecord = false;
                    sqlDBConnection.Open();
                    int records = (int)myCommand.ExecuteScalar();
                    if (records != 0)
                        foundRecord = true;
                    sqlDBConnection.Close();

                    object jsonObject = new { available = (!foundRecord) };
                    var json = new JavaScriptSerializer().Serialize(jsonObject);
                    return json.ToString();
                }
            }
            catch (Exception ex)
            {
                return string.Format("Exception : {0}", ex.Message);
            }
        }
    }
}

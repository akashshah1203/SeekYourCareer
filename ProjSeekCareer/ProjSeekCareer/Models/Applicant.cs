using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class Applicant
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public float SSCPercent { get; set; }
        public float HSCPercent { get; set; }
        public char GradComplete { get; set; }
        public float GradPercent { get; set; }
        public char PGComplete { get; set; }
        public float PGPercent { get; set; }
        public char HaveWorkExp { get; set; }
        public int WorkExpYears { get; set; }
    }
}
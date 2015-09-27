using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class SelJobApplicants
    {
        public int ApplicantId { get; set; }
        public int UserID { get; set; }
        public string JobId { get; set; }
        public string AppDate { get; set; }
        public string Correspondance { get; set; }
        public string Status { get; set; }
        
        public string Name { get; set; }
        public int Age { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
    }
}
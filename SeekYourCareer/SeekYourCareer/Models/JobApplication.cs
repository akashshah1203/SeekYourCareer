using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class JobApplication
    {
        public int ApplicantId { get; set; }
        public int UserID { get; set; }
        public string JobId { get; set; }
        public DateTime AppDate { get; set; }
        public string Correspondance { get; set; }
        public string Status { get; set; }
     }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class JobApplication
    {
        public string ApplicantId { get; set; }
        public int UserID { get; set; }
        public string JobId { get; set; }
        public DateTime AppDate { get; set; }
        public string Correspondance { get; set; }
        public string Status { get; set; }
     }
}


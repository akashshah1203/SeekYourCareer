using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class WSApplication
    {
        public string ApplicantId { get; set; }
        public int UserId { get; set; }
        public string WorkshopId { get; set; }
        public DateTime AppDate { get; set; }
        public string Status { get; set; }
    }
}
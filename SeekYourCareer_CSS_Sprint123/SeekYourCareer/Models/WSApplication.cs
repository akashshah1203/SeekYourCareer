using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WSApplication
    {
        public int ApplicantId { get; set; }
        public int UserId { get; set; }
        public int WorkshopId { get; set; }
        public DateTime AppDate { get; set; }
        public string Status { get; set; }
    }
}
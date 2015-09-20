using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class TrainingDetailsTable
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public char Graduation { get; set; }
        public char PostGraduation { get; set; }
        public int PastExp { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
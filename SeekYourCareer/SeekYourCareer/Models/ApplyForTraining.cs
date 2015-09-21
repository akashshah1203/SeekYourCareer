using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class ApplyForTraining
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string EmailID { get; set; }
        public string Contact { get; set; }
        public decimal SSCPercentage { get; set; }
        public decimal HscPercent { get; set; }
        public decimal GraduationPercent { get; set; }
        public decimal PostGradPercent { get; set; }
        public  int Experience { get; set; }
        public string Company { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public string Domain { get; set; }
        public string TrainingID { get; set; }
        
    }
}
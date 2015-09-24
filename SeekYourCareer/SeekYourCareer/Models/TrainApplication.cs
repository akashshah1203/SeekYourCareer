using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class TrainApplication
    {
        public int ApplicantId { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string TrainingId { get; set; }
        public string AppDate { get; set; }
        public string CorrAddress { get; set; }
        public string ContactNo { get; set; }
        public string SelectionStatus { get; set; }
     }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class Training
    {
        public string TrainingID { get; set; }
        public int RepId { get; set; }
        public string Domain { get; set; }
        public char Graduation { get; set; }
        public char PG { get; set; }
        public int PastExp { get; set; }
        public DateTime StartingDate { get; set; }
        public int Duration { get; set; }
        public int NoOfSeat { get; set; }
        public string TrainingDesc { get; set; }
        public string StaffApproval { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WorkshopTable
    {
        public int WorkshopID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfSeats { get; set; }
        public decimal MinGrad { get; set; }
        public decimal MinPostGrad { get; set; }
        public string Details { get; set; }

    }
}
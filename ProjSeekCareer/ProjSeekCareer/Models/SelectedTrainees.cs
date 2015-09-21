using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjSeekCareer.Models
{
    public class SelectedTrainees
    {
        public List<Company> CompanyModel { get; set; }
        public SelectList FilteredTraining { get; set; }
        public List<TrainingL> TrainingModel { get; set; }
    }

    public class Company
    {
        public string RepId { get; set; }
        public string CompanyName { get; set; }
    }

    public class TrainingL
    {
        public string RepId { get; set; }
        public string TrainingId { get; set; }
    }
}
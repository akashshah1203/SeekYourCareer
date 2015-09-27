using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class RepresentativeAddJobOffer
    {
        [Display(Name = "Company")]
        public string Company { get; set; }


        [Display(Name = "StreamCode")]
        public string streamName { get; set; }

        
        [Required]
        [Display(Name = "Job Offer ID")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum 2 and atmost 20 characters allowed.")]
        public string jobOfferID { get; set; }


        [Display(Name = "Job Type")]
        public string jobType { get; set; }

        [Required]
        [Display(Name = "Minimum SSC Percentage")]
        [Range(0.0, 100.0)]
        public float SscPercentage { get; set; }

        [Required]
        [Display(Name = "Minimum Hsc Percentage  ")]
        [Range(0.0, 100.0)]
        public float HscPercentage { get; set; }

        [Required]
        [Display(Name = "Minimum Graduation Perecentage ")]
        [Range(0.0, 100.0, ErrorMessage = "Only between 0-100.")]
        public float GraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Minimum Post Graduation Percentage  ")]
        [Range(0.0, 100.0, ErrorMessage = "Only between 0-100.")]
        public float PostGraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Salary Per Month")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Only positive Integer is allowed.")]
        public int monthlySalary { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }
    
        [Required]
        [Display(Name = "Experience")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Only positive Integer is allowed.")]
        public int Experience { get; set; }


        [Required]
        [Display(Name = "Last Date Of Application")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime LastDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Models
{
    public class Applicant
    {
        [Required]
        [Display(Name="User Name")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphabets, numbers and underscore allowed.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        [Compare("Password",ErrorMessage="Password not match")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "Atmost 30 characters allowed.")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        //[MinimumValue(18)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [Display(Name = "Contact ID")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string ContactNumber { get; set; }
        
        [Required]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailId { get; set; }
        
        [Required]
        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Atmost 50 characters allowed.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Required]
        [Display(Name = "Ssc Percentage  ")]
        [Range(0.0, 100.0)]
        public float SscPercentage { get; set; }


        [Required]
        [Display(Name = "Hsc Percentage  ")]
        [Range(0.0, 100.0)]
        public float HscPercentage { get; set; }

        [Required]
        [Display(Name = "Completed Graduation ? ")]
        public bool CompletedGraduation { get; set; }


        [Required]
        [Display(Name = "Graduation Perecentage ")]
        [Range(0.0, 100.0)]
        public float GraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Completed Post Graduation ? ")]
        public bool CompletedPostGraduation { get; set; }


        [Required]
        [Display(Name = "Post Graduation Percentage  ")]
        [Range(0.0, 100.0)]
        public float PostGraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Previous Work Experience ? ")]
        public bool PreviousWorkExperience { get; set; }

        [Required]
        [Display(Name = "Work Experience ")]
        [Range(0, Int32.MaxValue)]
        public int WorkExperience { get; set; }


    }
}
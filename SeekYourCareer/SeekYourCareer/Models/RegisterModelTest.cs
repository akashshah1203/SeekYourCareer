using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Models
{
    public class RegisterModelTest 
    {
        [Display(Name = "User Name")]
        [RegularExpression(@"^[a-zA-Z_]*$", ErrorMessage = "Only alphabets and numbers allowed.")]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]+$", ErrorMessage = "Only alphabets and numbers allowed. Must contain atleast 1 alphabet and 1 number.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        [Compare("Password",ErrorMessage="Password not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Contact ID")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email ID")]
        public string EmailId { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Ssc Percentage  ")]
        public float SscPercentage { get; set; }


        [Display(Name = "Hsc Percentage  ")]
        public float HscPercentage { get; set; }

        [Display(Name = "Completed Graduation ? ")]
        public bool CompletedGraduation { get; set; }


        [Display(Name = "Graduation Perecentage ")]
        public float GraduationPercentage { get; set; }


        [Display(Name = "Completed Post Graduation ? ")]
        public bool CompletedPostGraduation { get; set; }


        [Display(Name = "Post Graduation Percentage  ")]
        public float PostGraduationPercentage { get; set; }


        [Display(Name = "Previous Work Experience ? ")]
        public bool PreviousWorkExperience { get; set; }


        [Display(Name = "Work Experience ")]
        public int WorkExperience { get; set; }

    
    }
}
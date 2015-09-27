using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Models
{
    public class ChangePassword
    {
        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Re-enter New Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [Compare("New Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }
    }
}
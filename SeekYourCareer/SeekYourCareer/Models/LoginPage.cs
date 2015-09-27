using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace SeekYourCareer.Models
{
    public class LoginPage
    {
        [Required]
        [Display(Name = "Type Of User")]
        public string TypeOfUser { get; set; }


        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[a-zA-Z_]*$", ErrorMessage = "Only alphabets and underscore allowed.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string UserName { get; set; }

        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]+$", ErrorMessage = "Only alphabets and numbers allowed. Must contain atleast 1 alphabet and 1 number.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string Password { get; set; }
    }
}
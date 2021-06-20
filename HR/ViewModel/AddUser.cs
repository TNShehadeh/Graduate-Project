using HR.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class AddUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public DateTime Bday { get; set; }
        [Display(Name = "Gender")]
        public int Gender { get; set; }
        [Display(Name = "Social Status")]
        public int SocialStatus { get; set; }
        [Display(Name = "Department")]
        public int DepartmentName { get; set; }

        public IFormFile Photo { get; set; }
    }
}

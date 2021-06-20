using HR.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class AddUserDetails
    {
                //--- Address ---\\

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }

                //--- BankInfo ---\\

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

             //--- OfficialDetails ---\\

        [Required]
        [Display(Name = "Employee Type")]
        public int EmployeeTypeId { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        [Required]
        [Display(Name = "Emlpoyee Grade")]
        public int EmlpoyeeGradeId { get; set; }

        [Required]
        [Display(Name = "EmlpoyeeGrade")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        [Required]
        [Display(Name = "PF Number")]
        public string PFNumber { get; set; }
        [Required]
        [Display(Name = "Shift")]
        public int ShiftId { get; set; }
        [Required]
        [Display(Name = "SuperVisor")]
        public string SuperEmlpoyeeId { get; set; }
        [Required]
        public double TotalVacations { get; set; }
        public double TotoleHouresToleave { get; set; }
        [Required]
        public int Sickleave { get; set; }

        //--- Documents ---\\

        [Required]
        [Display(Name = "The Document Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "The Document")]
        public IFormFile TheDocument { get; set; }
    }
}

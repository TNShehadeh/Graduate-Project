using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class EditUserDetailsViewModel
    {
        //--- User ---\\
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string FatherName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime Bday { get; set; }

        public string Gender { get; set; }

        public string SocialStatus { get; set; }

        public string DepartmentName { get; set; }

        public IFormFile Photo { get; set; }
        //--- Address ---\\

        public string Country { get; set; }


        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PinCode { get; set; }

        //--- BankInfo ---\\

        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string AccountName { get; set; }

        //--- OfficialDetails ---\\

        public int EmployeeTypeId { get; set; }

        public int DesignationId { get; set; }

        public int EmlpoyeeGradeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        public string PFNumber { get; set; }

        public int ShiftId { get; set; }

        public string SuperEmlpoyeeId { get; set; }

        public double TotalVacations { get; set; }
        public double TotoleHouresToleave { get; set; }
        public int Sickleave { get; set; }


        //--- Documents ---\\

        public string Name { get; set; }

        public IFormFile TheDocument { get; set; }
    }
}

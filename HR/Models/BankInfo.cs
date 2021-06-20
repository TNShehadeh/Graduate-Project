using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class BankInfo
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}

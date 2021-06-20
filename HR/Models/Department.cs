using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "The manager email")]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "When he start")]
        public DateTime MngStartDate { get; set; }
    }
}

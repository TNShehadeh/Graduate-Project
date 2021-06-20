using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class OfficialDetails
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Required]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        [Required]
        public int EmlpoyeeGradeId { get; set; }
        public EmlpoyeeGrade EmlpoyeeGrade { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        [Required]
        public string PFNumber { get; set; }
        [Required]
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        [Required]
        public string SuperEmlpoyeeId { get; set; }
        [Required]
        public double TotalVacations { get; set; }
        [Required]
        public double TotoleHouresToleave { get; set; }
        [Required]
        public int Sickleave { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

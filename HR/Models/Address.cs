using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        public string PinCode { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

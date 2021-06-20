using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Documents
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TheDocumentPath { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

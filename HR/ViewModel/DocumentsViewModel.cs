using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class DocumentsViewModel
    {
        [Required]
        [Display(Name = "The Document Name")]
        public string Name { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        [Display(Name = "The Document")]
        public IFormFile TheDocument { get; set; }
    }
}

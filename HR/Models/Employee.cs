
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Employee : IdentityUser
    {

        public string FirstName { get; set; }

        public string FatherName { get; set; } 
        public string MiddleName { get; set; }
     
        [DataType(DataType.Date)]
        public DateTime Bday { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public SocialStatus SocialStatus { get; set; }
        public int SocialStatusId { get; set; }
        
        public string PhotoPath { get; set; }
        public int DepartmentName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Key]
        public string RoleName { get; set; }
    }
}

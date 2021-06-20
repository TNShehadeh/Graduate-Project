using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Clims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BDay { get; set; }
        public string Adress { get; set; }
        public List<string> Clims { get; set; }
        public List<string> Roles { get; set; }
    }
}

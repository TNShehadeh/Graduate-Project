using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]

        [DataType(DataType.Time)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime End { get; set; }
    }
}

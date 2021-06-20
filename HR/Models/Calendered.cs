using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Calendered
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime EndDate { get; set; }
        public bool IsAllDay { get; set; }
        public string Description { get; set; }
        public string UserInfo { get; set; }
    }
}

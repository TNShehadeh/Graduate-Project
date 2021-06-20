using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string ToUser { get; set; }
        public string FromUser { get; set; }
        public string ODate { get; set; }
        public string PhotoPath { get; set; }
        public bool IsRead { get; set; }

    }
}

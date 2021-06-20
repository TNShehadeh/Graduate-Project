﻿using HR.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.ViewModel
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public string EmployeeInfo { get; set; }
        public string SuperEmployeeInfo { get; set; }
        public double LeaveCount { get; set; }
        public double HouresCount { get; set; }
        /*[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]*/
        public DateTime Start { get; set; }
        /*[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]*/
        public DateTime End { get; set; }
        public string Reason { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string Status { get; set; }
        public IFormFile LeaveDocument { get; set; }
    }
}

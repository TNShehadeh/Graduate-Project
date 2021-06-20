using HR.Data;
using HR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Hubs
{
    public class nHub : Hub
    {
        private readonly UserManager<Employee> userManger;
        private readonly ApplicationDbContext context;
        public nHub(UserManager<Employee> userManger, ApplicationDbContext context)
        {
            this.userManger = userManger;
            this.context = context;
        }
        public async Task LeaveApplications(string Id, string myId)
        {
            var sender = await userManger.FindByIdAsync(myId);
            DateTime today = DateTime.Today;
            var n = new noti {
                Tiltle = sender.Email,
                Text = "New applied",
                PhotoPath = sender.PhotoPath,
                Time = DateTime.Now.ToString("yyyy:MM:dd HH:mm"),
            };
             
            await Clients.User(Id).SendAsync("ReciveApp", n.Text, n.Tiltle, n.PhotoPath, n.Time);
            context.Notification.Add(new Notification { FromUser = myId, PhotoPath = sender.PhotoPath, ToUser = Id, ODate = n.Time , IsRead = false });
            await context.SaveChangesAsync();
        }
    }
    class noti
    {
        public string Tiltle { get; set; }
        public string Text { get; set; }
        public string PhotoPath { get; set; }
        public  string Time { get; set; }
    }
}



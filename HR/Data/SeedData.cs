using HR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Data
{
    public class SeedData : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Employee> userManager;
        public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task SeedUsers()
        {
            Employee employee = new Employee
            {
                Id = "0",
                Email = "Admin@admin.com",
                UserName = "Admin@admin.com",
                FirstName = "Admin",
                FatherName = "Admin",
                MiddleName = "Admiin",
                GenderId = 1,
                SocialStatusId = 1,
                Bday = DateTime.Now,
                DepartmentName = 1,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(employee, "123456");
        }
    }
}

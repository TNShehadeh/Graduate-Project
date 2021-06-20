using HR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Adress { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }
        public DbSet<OfficialDetails> OfficialDetails { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<SocialStatus> SocialStatus { get; set; }
        private void SeedUsers(ModelBuilder builder)
        {
            Employee employee = new Employee
            {
                Id = "0",
                Email = "Admin@admin.admin",
                UserName = "Admin@admin.admin",
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
            PasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
            passwordHasher.HashPassword(employee, "123456");
            builder.Entity<Employee>().HasData(employee);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "testRole", ConcurrencyStamp = "1", NormalizedName = "testRole" });
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "1", UserId = "0" }
                );
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);

            builder.Entity<Department>().HasData(
               new Department
               {
                   Id = 1,
                   Name = "Admin",
                   EmployeeId = "0",
                   MngStartDate = DateTime.Now
               });
        }

        public DbSet<EmployeeType> EmployeeType { get; set; }

        public DbSet<Designation> Designation { get; set; }

        public DbSet<EmlpoyeeGrade> EmlpoyeeGrade { get; set; }

        public DbSet<Shift> Shift { get; set; }

        public DbSet<Calendered> Calendered { get; set; }

        public DbSet<LeaveApplication> LeaveApplication { get; set; }

        public DbSet<LeaveType> LeaveType { get; set; }

        public DbSet<Status> Status { get; set; }
        public DbSet<Notification> Notification { get; set; }
    }
}

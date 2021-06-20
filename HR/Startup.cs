using HR.Data;
using HR.Hubs;
using HR.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<Employee, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddTokenProvider<DataProtectorTokenProvider<Employee>>(TokenOptions.DefaultProvider)
               .AddDefaultUI();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            services.AddControllersWithViews();
            services.AddAuthorization(options => {
                options.AddPolicy("DeleteRole", policy => policy.RequireClaim("Delete Role permission"));
                options.AddPolicy("CreateRole", policy => policy.RequireClaim("Create Role permission"));
                options.AddPolicy("EditRole", policy => policy.RequireClaim("Edit Role permission"));
                options.AddPolicy("EditUsersInRole", policy => policy.RequireClaim("EditUsersInRole permission"));
                options.AddPolicy("ManageUserClaims", policy => policy.RequireClaim("ManageUserClaims permission"));
                options.AddPolicy("AddUser", policy => policy.RequireClaim("AddUser permission"));
                options.AddPolicy("DeleteUser", policy => policy.RequireClaim("DeleteUser permission"));
                options.AddPolicy("EditUser", policy => policy.RequireClaim("EditUser permission"));
                options.AddPolicy("ViewEditUser", policy => policy.RequireClaim("ViewEditUser permission"));
                options.AddPolicy("ManageUserRoles", policy => policy.RequireClaim("ManageUserRoles permission"));
                options.AddPolicy("DownloadAsExel", policy => policy.RequireClaim("DownloadAsExel permission"));
                options.AddPolicy("AddUserDetails", policy => policy.RequireClaim("AddUserDetails permission"));
                options.AddPolicy("EditUserDetails", policy => policy.RequireClaim("EditUserDetails permission"));
                options.AddPolicy("DownloadAllAsExel", policy => policy.RequireClaim("DownloadAllAsExel permission"));
                options.AddPolicy("Edit&DeleteLeave", policy => policy.RequireClaim("Edit&DeleteLeave permission"));
                options.AddPolicy("Edit&AndOfficialDetails", policy => policy.RequireClaim("Edit&AndOfficialDetails permission"));
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
        
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Calendereds}/{action=CalenderedView}");
                endpoints.MapRazorPages();
                endpoints.MapHub<nHub>("/nHub");
            });
        }
    }
}

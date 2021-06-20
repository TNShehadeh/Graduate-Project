using HR.Data;
using HR.Models;
using HR.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,HR,Manager,User")]
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> userManager;
        private readonly IWebHostEnvironment environment;

        public LeaveApplicationsController(ApplicationDbContext context, UserManager<Employee> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            this.userManager = userManager;
            this.environment = environment;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index(int? id)
        {
            if(id != null)
            {
                var notification = await _context.Notification
                .FirstOrDefaultAsync(m => m.Id == id);
                notification.IsRead = true;
                _context.Update(notification);
                _context.SaveChanges();
            }
            var officialDetails = await _context.OfficialDetails
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(m => m.SuperEmlpoyeeId == userManager.GetUserId(User));
            if(officialDetails != null)
            {
                ViewBag.super = officialDetails.SuperEmlpoyeeId;
            }


            var applicationDbContext = _context.LeaveApplication.Include(l => l.LeaveType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        [Authorize(Roles = "Admin,HR,Manager,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplication
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public async Task<IActionResult> Create()
        {
            var user = userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User)).Result;
            var officialDetails = await _context.OfficialDetails
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(m => m.EmployeeId == user.Id);
            ViewBag.SuperId = officialDetails.SuperEmlpoyeeId;
            var model = new LeaveViewModel
            {
                LeaveCount = officialDetails.TotalVacations,
                HouresCount = officialDetails.TotoleHouresToleave, 
                Start = DateTime.Now.Date,
                End = DateTime.Now.Date,
            };
            if (officialDetails.TotalVacations <= 0 && officialDetails.TotoleHouresToleave <= 0 && officialDetails.Sickleave <= 0)
            {
                ViewBag.MyMessage = "No Vacations for " + user.Email;
                ViewBag.Class = "text-danger";
                ViewBag.TotalVacations = officialDetails.TotalVacations;
                ViewBag.TotoleHouresToleave = officialDetails.TotoleHouresToleave;
                ViewBag.Sickleave = officialDetails.Sickleave;
            }
            else
            {
                ViewBag.MyMessage = "Total Vacations for" + user.Email +" : " + officialDetails.TotalVacations;
                ViewBag.Class = "text-primary";
                ViewBag.TotalVacations = officialDetails.TotalVacations;
                ViewBag.TotoleHouresToleave = officialDetails.TotoleHouresToleave;
                ViewBag.Sickleave = officialDetails.Sickleave;
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Type");
            return View(model);
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeInfo,LeaveCount,Start,End,Reason,LeaveTypeId,Status,HouresCount")] LeaveApplication leaveApplication,LeaveViewModel model)
        {
            var user = userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User)).Result;
            var officialDetails = await _context.OfficialDetails
                                        .Include(a => a.Employee)
                                        .FirstOrDefaultAsync(m => m.EmployeeId == user.Id);
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.LeaveDocument != null)
                {
                    string uploadsFolder = Path.Combine(environment.WebRootPath, "Files/LeaveDocuments");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.LeaveDocument.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.LeaveDocument.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (ModelState.IsValid)
                {
                    LeaveApplication newLeaveApplication = new LeaveApplication
                    {
                        EmployeeInfo = userManager.GetUserId(HttpContext.User),
                        Start = leaveApplication.Start,
                        End = leaveApplication.End,
                        Status = "Pending",
                        LeaveTypeId = leaveApplication.LeaveTypeId,
                        Reason = leaveApplication.Reason,
                        LeaveCount = officialDetails.TotalVacations,
                        HouresCount = officialDetails.TotoleHouresToleave,
                        SuperEmployeeInfo = officialDetails.SuperEmlpoyeeId,
                        LeaveDocumentPath = uniqueFileName
                    };
                    _context.Add(newLeaveApplication);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Type", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        [Authorize(Roles = "Admin,HR,Manager")]
        [Authorize(Policy = "Edit&DeleteLeave")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplication.FindAsync(id);
            var user = userManager.FindByIdAsync(leaveApplication.EmployeeInfo).Result;
            var officialDetails = await _context.OfficialDetails
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeId == user.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            if (officialDetails.TotalVacations <= 0 && officialDetails.TotoleHouresToleave <= 0 && officialDetails.Sickleave <= 0)
            {
                ViewBag.MyMessage = "No Vacations for " + user.Email;
                ViewBag.Class = "text-danger";
            }
            else
            {
                ViewBag.MyMessage = user.Email + " have " + officialDetails.TotalVacations +" Day and " + officialDetails.TotoleHouresToleave + "Hour";
                ViewBag.Class = "text-primary";
            }

            if (leaveApplication.Start.Date < DateTime.Now.Date || leaveApplication.Status != "Pending")
            {
                return RedirectToAction("Details", new { leaveApplication.Id});
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Type", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&DeleteLeave")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeInfo,LeaveCount,Start,End,Reason,LeaveTypeId,Status,LeaveDocumentPath")] LeaveApplication leaveApplication, bool Sstatus)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }
            int totalHours = (leaveApplication.End - leaveApplication.Start).Hours;
            int totalDays = (leaveApplication.End - leaveApplication.Start).Days;
            int totalMinutes = (leaveApplication.End - leaveApplication.Start).Minutes;
            if (totalDays < 0)
            {
                totalDays = totalDays * -1;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = userManager.FindByIdAsync(leaveApplication.EmployeeInfo).Result;
                    var officialDetails = await _context.OfficialDetails
                           .Include(a => a.Employee)
                           .FirstOrDefaultAsync(m => m.EmployeeId == user.Id);
                    var calendered = new Calendered {};
                    if (leaveApplication.Start.Date >= DateTime.Now.Date || leaveApplication.Status == "Pending")
                    {
                        if (Sstatus && leaveApplication.Status != "Accepted")
                        {
                            if (leaveApplication.LeaveTypeId == 4)
                            {
                                if (totalDays == 1)
                                {
                                    officialDetails.Sickleave -= totalDays;
                                    leaveApplication.Status = "Accepted";
                                }
                                else if (totalDays > 1)
                                {
                                    totalDays += 1;
                                    officialDetails.Sickleave -= totalDays;
                                    leaveApplication.Status = "Accepted";
                                }
                                if (totalHours > 4)
                                {
                                    officialDetails.TotalVacations = leaveApplication.LeaveCount - 1;
                                    leaveApplication.Status = "Accepted";
                                }
                                else
                                {
                                    officialDetails.TotoleHouresToleave -= totalHours;
                                    if (totalMinutes < 30 && totalMinutes > 20)
                                    {
                                        officialDetails.TotoleHouresToleave -= 0.5;
                                    }
                                    else if (totalMinutes > 30)
                                    {
                                        officialDetails.TotoleHouresToleave--;
                                    }
                                    leaveApplication.Status = "Accepted";
                                }
                                calendered = new Calendered
                                {
                                    StartDate = leaveApplication.Start,
                                    EndDate = leaveApplication.End,
                                    Name = "Sick Veccation for " + user.Email,
                                    UserInfo = officialDetails.EmployeeId
                                };
                            }
                            else
                            {
                                if (totalDays == 1)
                                {
                                    officialDetails.TotalVacations -= totalDays;
                                    leaveApplication.Status = "Accepted";
                                    if (totalHours > 4)
                                    {
                                        officialDetails.TotalVacations = leaveApplication.LeaveCount - 1;
                                        leaveApplication.Status = "Accepted";
                                    }
                                    else
                                    {
                                        officialDetails.TotoleHouresToleave -= totalHours;
                                        if (totalMinutes < 30 && totalMinutes > 20)
                                        {
                                            officialDetails.TotoleHouresToleave -= 0.5;
                                        }
                                        else if (totalMinutes > 30)
                                        {
                                            officialDetails.TotoleHouresToleave--;
                                        }
                                        leaveApplication.Status = "Accepted";
                                    }
                                    calendered = new Calendered
                                    {
                                        StartDate = leaveApplication.Start,
                                        EndDate = leaveApplication.End,
                                        Name = "Veccation for " + user.Email,
                                        UserInfo = officialDetails.EmployeeId
                                    };
                                }
                                else if (totalDays > 1)
                                {
                                    totalDays += 1;
                                    officialDetails.TotalVacations -= totalDays;
                                    leaveApplication.Status = "Accepted";
                                    if (totalHours > 4)
                                    {
                                        officialDetails.TotalVacations = leaveApplication.LeaveCount - 1;
                                        leaveApplication.Status = "Accepted";
                                    }
                                    else
                                    {
                                        officialDetails.TotoleHouresToleave -= totalHours;
                                        if (totalMinutes < 30 && totalMinutes > 20)
                                        {
                                            officialDetails.TotoleHouresToleave -= 0.5;
                                        }
                                        else if (totalMinutes > 30)
                                        {
                                            officialDetails.TotoleHouresToleave--;
                                        }
                                        leaveApplication.Status = "Accepted";
                                    }
                                    calendered = new Calendered
                                    {
                                        StartDate = leaveApplication.Start,
                                        EndDate = leaveApplication.End,
                                        Name = "Veccation for " + user.Email,
                                        UserInfo = officialDetails.EmployeeId
                                    };
                                }
                                else
                                {
                                    if (totalHours > 4)
                                    {
                                        officialDetails.TotalVacations = leaveApplication.LeaveCount - 1;
                                        leaveApplication.Status = "Accepted";
                                    }
                                    else
                                    {
                                        officialDetails.TotoleHouresToleave -= totalHours;
                                        if (totalMinutes < 30 && totalMinutes > 20)
                                        {
                                            officialDetails.TotoleHouresToleave -= 0.5;
                                        }
                                        else if (totalMinutes > 30)
                                        {
                                            officialDetails.TotoleHouresToleave--;
                                        }
                                        leaveApplication.Status = "Accepted";
                                    }
                                    calendered = new Calendered
                                    {
                                        StartDate = leaveApplication.Start,
                                        EndDate = leaveApplication.End,
                                        Name = "Leave for " + user.Email,
                                        UserInfo = officialDetails.EmployeeId
                                    };
                                }
                            }
                        }
                        else if (Sstatus == false && leaveApplication.Status == "Pending")
                        {
                            leaveApplication.Status = "Rejected";
                        }
                        else
                        {
                            leaveApplication.Status = "Pending";
                        }
                        if (officialDetails.TotoleHouresToleave <= -8 && officialDetails.TotalVacations > 0)
                        {
                            officialDetails.TotalVacations--;
                            officialDetails.TotoleHouresToleave = 0;
                        }
                    }
                    _context.Update(officialDetails);
                    _context.Add(calendered);
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Type", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }
        // POST: LeaveApplications/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&DeleteLeave")]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveApplication = await _context.LeaveApplication.FindAsync(id);
            _context.LeaveApplication.Remove(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplication.Any(e => e.Id == id);
        }
        public async Task<IActionResult> DownloadAsExel(LeaveApplication leaveApplication)
        {
            var leaveApp = await _context.LeaveApplication.ToListAsync();
            var builder = new StringBuilder();
            builder.AppendLine("employee ,super Employee, stutes, Start, End ");
            foreach (var Leave in leaveApp)
            {
                if (Leave.EmployeeInfo == userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    var start = Leave.Start;
                    var end = Leave.End;
                    string employee = _context.Users.Where(u => u.Id == Leave.EmployeeInfo).Select(u => u.UserName).SingleOrDefault();
                    string SuperEmployee = _context.Users.Where(u => u.Id == Leave.SuperEmployeeInfo).Select(u => u.UserName).SingleOrDefault();
                    var stutes = Leave.Status;
                    builder.AppendLine($"{employee},{SuperEmployee},{stutes},{start},{end}");
                }
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "LeavesInfo.csv");
        }
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "DownloadAllAsExel")]
        public async Task<IActionResult> DownloadAllAsExel(LeaveApplication leaveApplication)
        {
            var leaveApp = await _context.LeaveApplication.ToListAsync();
            var builder = new StringBuilder();
            builder.AppendLine("employee ,super Employee, stutes, Start, End ");
            foreach (var Leave in leaveApp)
            {
                    var start = Leave.Start;
                    var end = Leave.End;
                    string employee = _context.Users.Where(u => u.Id == Leave.EmployeeInfo).Select(u => u.UserName).SingleOrDefault();
                    string SuperEmployee = _context.Users.Where(u => u.Id == Leave.SuperEmployeeInfo).Select(u => u.UserName).SingleOrDefault();
                    var stutes = Leave.Status;
                    builder.AppendLine($"{employee},{SuperEmployee},{stutes},{start},{end}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "LeavesInfo.csv");
        }
    }
}
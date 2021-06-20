using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR.Data;
using HR.Models;
using Microsoft.AspNetCore.Authorization;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class OfficialDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficialDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfficialDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OfficialDetails.Include(o => o.Designation).Include(o => o.EmlpoyeeGrade).Include(o => o.Employee).Include(o => o.EmployeeType).Include(o => o.Shift);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OfficialDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialDetails = await _context.OfficialDetails
                .Include(o => o.Designation)
                .Include(o => o.EmlpoyeeGrade)
                .Include(o => o.Employee)
                .Include(o => o.EmployeeType)
                .Include(o => o.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officialDetails == null)
            {
                return NotFound();
            }

            return View(officialDetails);
        }

        // GET: OfficialDetails/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name");
            ViewData["EmlpoyeeGradeId"] = new SelectList(_context.EmlpoyeeGrade, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name");
            ViewData["SuperEmlpoyeeId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name");
            return View();
        }

        // POST: OfficialDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("Id,EmployeeTypeId,DesignationId,EmlpoyeeGradeId,JoinDate,PFNumber,ShiftId,SuperEmlpoyeeId,EmployeeId,TotalVacations")] OfficialDetails officialDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officialDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", officialDetails.DesignationId);
            ViewData["EmlpoyeeGradeId"] = new SelectList(_context.EmlpoyeeGrade, "Id", "Name", officialDetails.EmlpoyeeGradeId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", officialDetails.EmployeeId);
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name", officialDetails.EmployeeTypeId);
            ViewData["SuperEmlpoyeeId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", officialDetails.ShiftId);
            return View(officialDetails);
        }

        // GET: OfficialDetails/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialDetails = await _context.OfficialDetails.FindAsync(id);
            if (officialDetails == null)
            {
                return NotFound();
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", officialDetails.DesignationId);
            ViewData["EmlpoyeeGradeId"] = new SelectList(_context.EmlpoyeeGrade, "Id", "Name", officialDetails.EmlpoyeeGradeId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", officialDetails.EmployeeId);
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name", officialDetails.EmployeeTypeId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", officialDetails.ShiftId);
            ViewData["SuperEmlpoyeeId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(officialDetails);
        }

        // POST: OfficialDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeTypeId,DesignationId,EmlpoyeeGradeId,JoinDate,PFNumber,ShiftId,SuperEmlpoyeeId,EmployeeId")] OfficialDetails officialDetails)
        {
            if (id != officialDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officialDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficialDetailsExists(officialDetails.Id))
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
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", officialDetails.DesignationId);
            ViewData["EmlpoyeeGradeId"] = new SelectList(_context.EmlpoyeeGrade, "Id", "Name", officialDetails.EmlpoyeeGradeId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", officialDetails.EmployeeId);
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name", officialDetails.EmployeeTypeId);
            ViewData["SuperEmlpoyeeId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", officialDetails.ShiftId);
            return View(officialDetails);
        }

        // POST: OfficialDetails/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var officialDetails = await _context.OfficialDetails.FindAsync(id);
            _context.OfficialDetails.Remove(officialDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficialDetailsExists(int id)
        {
            return _context.OfficialDetails.Any(e => e.Id == id);
        }
    }
}

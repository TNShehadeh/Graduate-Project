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
    public class EmployeeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeType.ToListAsync());
        }

        // GET: EmployeeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _context.EmployeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return View(employeeType);
        }

        // GET: EmployeeTypes/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("Id,Name")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeType);
        }

        // GET: EmployeeTypes/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _context.EmployeeType.FindAsync(id);
            if (employeeType == null)
            {
                return NotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EmployeeType employeeType)
        {
            if (id != employeeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTypeExists(employeeType.Id))
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
            return View(employeeType);
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeType = await _context.EmployeeType.FindAsync(id);
            _context.EmployeeType.Remove(employeeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTypeExists(int id)
        {
            return _context.EmployeeType.Any(e => e.Id == id);
        }
    }
}

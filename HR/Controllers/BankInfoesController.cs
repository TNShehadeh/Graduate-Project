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
    public class BankInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankInfoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BankInfo.Include(b => b.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BankInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankInfo = await _context.BankInfo
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (bankInfo == null)
            {
                return NotFound();
            }

            return View(bankInfo);
        }

        // GET: BankInfoes/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: BankInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("AccountNumber,BankName,BranchName,AccountName,EmployeeId")] BankInfo bankInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Email", bankInfo.EmployeeId);
            return View(bankInfo);
        }

        // GET: BankInfoes/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bankInfo = await _context.BankInfo
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (bankInfo == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Email", bankInfo.EmployeeId);
            return View(bankInfo);
        }

        // POST: BankInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(string id, [Bind("AccountNumber,BankName,BranchName,AccountName,EmployeeId")] BankInfo bankInfo)
        {
            if (id != bankInfo.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankInfoExists(bankInfo.AccountNumber))
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
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Email", bankInfo.EmployeeId);
            return View(bankInfo);
        }

        // POST: BankInfoes/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var bankInfo = await _context.BankInfo.FindAsync(id);
            _context.BankInfo.Remove(bankInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankInfoExists(string id)
        {
            return _context.BankInfo.Any(e => e.AccountNumber == id);
        }
    }
}

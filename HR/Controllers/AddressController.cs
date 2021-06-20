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
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Adress.Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Adress
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("Id,Country,State,City,Street,PinCode,EmployeeId")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", address.EmployeeId);
            return View(address);
        }

        // GET: Address/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Adress.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", address.EmployeeId);
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,State,City,Street,PinCode,EmployeeId")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", address.EmployeeId);
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _context.Adress.FindAsync(id);
            _context.Adress.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Adress.Any(e => e.Id == id);
        }
    }
}

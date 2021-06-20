using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR.Data;
using HR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HR.Controllers
{
    [Authorize]
    public class CalenderedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> userManager;

        public CalenderedsController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Calendereds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calendered.ToListAsync());
        }
        public async Task<IActionResult> CalenderedView()
        {
            return View(await _context.Calendered.ToListAsync());
        }
        // GET: Calendereds/Details/5
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendered = await _context.Calendered
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendered == null)
            {
                return NotFound();
            }

            return View(calendered);
        }

        // GET: Calendereds/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calendereds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> CreateCalendar(Calendered model)
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            if (ModelState.IsValid)
            {
                Calendered newEvents = new Calendered
                {
                    Name = model.Name,
                    StartDate = model.StartDate,
                    IsAllDay = model.IsAllDay,
                    EndDate = model.EndDate,
                    Description = model.Description
                };
                _context.Add(newEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction("CalenderedView");
            }
            return View(model);
        }

        // GET: Calendereds/Edit/5
       /* public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendered = await _context.Calendered.FindAsync(id);
            if (calendered == null)
            {
                return NotFound();
            }
            return View(calendered);
        }*/

        // POST: Calendereds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int Id, DateTime start, DateTime end, string title)
        {
            var item = await _context.Calendered.FindAsync(Id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = title;
            item.StartDate = start;
            item.EndDate = end;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CalenderedView");
        }


        // POST: Calendereds/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int Id)
        {
            var item = await _context.Calendered.FindAsync(Id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CalenderedView");
        }
        private bool CalenderedExists(int id)
        {
            return _context.Calendered.Any(e => e.Id == id);
        }
    }
}

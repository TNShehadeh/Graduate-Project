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
    public class EmlpoyeeGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmlpoyeeGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmlpoyeeGrades
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmlpoyeeGrade.ToListAsync());
        }

        // GET: EmlpoyeeGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emlpoyeeGrade = await _context.EmlpoyeeGrade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emlpoyeeGrade == null)
            {
                return NotFound();
            }

            return View(emlpoyeeGrade);
        }

        // GET: EmlpoyeeGrades/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmlpoyeeGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("Id,Name")] EmlpoyeeGrade emlpoyeeGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emlpoyeeGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emlpoyeeGrade);
        }

        // GET: EmlpoyeeGrades/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emlpoyeeGrade = await _context.EmlpoyeeGrade.FindAsync(id);
            if (emlpoyeeGrade == null)
            {
                return NotFound();
            }
            return View(emlpoyeeGrade);
        }

        // POST: EmlpoyeeGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EmlpoyeeGrade emlpoyeeGrade)
        {
            if (id != emlpoyeeGrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emlpoyeeGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmlpoyeeGradeExists(emlpoyeeGrade.Id))
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
            return View(emlpoyeeGrade);
        }

        // POST: EmlpoyeeGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var emlpoyeeGrade = await _context.EmlpoyeeGrade.FindAsync(id);
            _context.EmlpoyeeGrade.Remove(emlpoyeeGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmlpoyeeGradeExists(int id)
        {
            return _context.EmlpoyeeGrade.Any(e => e.Id == id);
        }
    }
}

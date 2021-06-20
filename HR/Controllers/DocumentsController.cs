using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR.Data;
using HR.Models;
using HR.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> userManager;
        private readonly IWebHostEnvironment environment;

        public DocumentsController(ApplicationDbContext context, UserManager<Employee> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            this.userManager = userManager;
            this.environment = environment;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Documents.Include(d => d.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.Documents
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documents == null)
            {
                return NotFound();
            }

            return View(documents);
        }

        // GET: Documents/Create
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Create([Bind("Id,Name,TheDocumentPath,EmployeeId")] Documents documents, DocumentsViewModel model)
        {
            string uniqueFileName = null;
            if (model.TheDocument != null)
            {
                string uploadsFolder = Path.Combine(environment.WebRootPath, "Files/EmployeeDocuments");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.TheDocument.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.TheDocument.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var userDocuments = new Documents
            {
                Name = model.Name,
                TheDocumentPath = uniqueFileName,
                EmployeeId = model.EmployeeId
            };
            _context.Add(userDocuments);
            await _context.SaveChangesAsync();
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", documents.EmployeeId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Documents/Edit/5
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.Documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", documents.EmployeeId);
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TheDocumentPath,EmployeeId")] Documents documents)
        {
            if (id != documents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentsExists(documents.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "UserName", documents.EmployeeId);
            return View(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "Edit&AndOfficialDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var documents = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(documents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentsExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}

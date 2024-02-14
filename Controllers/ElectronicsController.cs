using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using ElectronicsStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ElectronicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectronicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Electronics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Electronics.Include(e => e.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Electronics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronic = await _context.Electronics
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electronic == null)
            {
                return NotFound();
            }

            return View(electronic);
        }

        // GET: Electronics/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName");
            return View();
        }

        // POST: Electronics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName,Price,Image,GenreId")] Electronic electronic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electronic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", electronic.GenreId);
            return View(electronic);
        }

        // GET: Electronics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronic = await _context.Electronics.FindAsync(id);
            if (electronic == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", electronic.GenreId);
            return View(electronic);
        }

        // POST: Electronics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName,Price,Image,GenreId")] Electronic electronic)
        {
            if (id != electronic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electronic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectronicExists(electronic.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", electronic.GenreId);
            return View(electronic);
        }

        // GET: Electronics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronic = await _context.Electronics
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electronic == null)
            {
                return NotFound();
            }

            return View(electronic);
        }

        // POST: Electronics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electronic = await _context.Electronics.FindAsync(id);
            if (electronic != null)
            {
                _context.Electronics.Remove(electronic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectronicExists(int id)
        {
            return _context.Electronics.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using farris_art_gallery.Data;
using farris_art_gallery.Models;

namespace farris_art_gallery
{
    public class FactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facts
        public async Task<IActionResult> Index()
        {
              return _context.Fact != null ? 
                          View
                          (
                              await _context.Fact
                                .Include(f => f.Artist)
                                .Include(f => f.Artwork)
                                .Include(f => f.Exhibit)
                                .ToListAsync()
                          ) :
                          Problem("Entity set 'ApplicationDbContext.Fact'  is null.");
        }

        // GET: Facts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fact == null)
            {
                return NotFound();
            }

            var fact = await _context.Fact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fact == null)
            {
                return NotFound();
            }

            return View(fact);
        }

        // GET: Facts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExhibitId,ArtistId,ArtworkId")] Fact fact)
        {
            fact.Artist = _context.Artist.FirstOrDefault(a => a.Id == fact.ArtistId);
            fact.Artwork = _context.Artwork.FirstOrDefault(a => a.Id == fact.ArtworkId);
            fact.Exhibit = _context.Exhibit.FirstOrDefault(e => e.Id == fact.ExhibitId);
            if (ModelState.IsValid)
            {
                _context.Add(fact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fact);
        }

        // GET: Facts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fact == null)
            {
                return NotFound();
            }

            var fact = await _context.Fact.FindAsync(id);
            if (fact == null)
            {
                return NotFound();
            }
            return View(fact);
        }

        // POST: Facts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Fact fact)
        {
            if (id != fact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactExists(fact.Id))
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
            return View(fact);
        }

        // GET: Facts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fact == null)
            {
                return NotFound();
            }

            var fact = await _context.Fact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fact == null)
            {
                return NotFound();
            }

            return View(fact);
        }

        // POST: Facts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fact == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fact'  is null.");
            }
            var fact = await _context.Fact.FindAsync(id);
            if (fact != null)
            {
                _context.Fact.Remove(fact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactExists(int id)
        {
          return (_context.Fact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using farris_art_gallery.Data;
using farris_art_gallery.Models;

namespace farris_art_gallery.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExhibitsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Exhibits
        public async Task<IActionResult> Index()
        {
              return _context.Exhibit != null ? 
                          View(await _context.Exhibit.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Exhibit'  is null.");
        }

        // GET: Exhibits/View/5
        public async Task<IActionResult> View(int? id)
        {
            if (id == null || _context.Exhibit == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exhibit == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit
                .Include(e => e.Facts)
                .ThenInclude(f => f.Artist)
                .Include(e =>e.Facts)
                .ThenInclude(f => f.Artwork)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }

        // GET: Exhibits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exhibits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,EndDate,ThumbnailFile")] Exhibit exhibit)
        {
            if (ModelState.IsValid)
            {
                if (exhibit.ThumbnailFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(exhibit.ThumbnailFile.FileName);
                    string extension = Path.GetExtension(exhibit.ThumbnailFile.FileName);
                    exhibit.ThumbnailName = fileName = "exhibit-" + exhibit.Id + extension;
                    string path = Path.Combine(wwwRootPath + "/img/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await exhibit.ThumbnailFile.CopyToAsync(fileStream);
                    }
                }
                _context.Add(exhibit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exhibit);
        }

        // GET: Exhibits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exhibit == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit.FindAsync(id);
            if (exhibit == null)
            {
                return NotFound();
            }
            return View(exhibit);
        }

        // POST: Exhibits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,ImageName")] Exhibit exhibit)
        {
            if (id != exhibit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exhibit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhibitExists(exhibit.Id))
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
            return View(exhibit);
        }

        // GET: Exhibits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exhibit == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }

        // POST: Exhibits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exhibit == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Exhibit'  is null.");
            }
            var exhibit = await _context.Exhibit.FindAsync(id);
            if (exhibit != null)
            {
                _context.Exhibit.Remove(exhibit);
                if (exhibit.ThumbnailName != null)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", exhibit.ThumbnailName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExhibitExists(int id)
        {
          return (_context.Exhibit?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

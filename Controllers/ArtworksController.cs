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
    public class ArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArtworksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Artworks
        public async Task<IActionResult> Index()
        {
              return _context.Artwork != null ? 
                          View(await _context.Artwork.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Artwork'  is null.");
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artwork == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .FirstOrDefaultAsync(a => a.Id == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // GET: Artworks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ThumbnailFile")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(artwork.ThumbnailFile.FileName);
                string extension = Path.GetExtension(artwork.ThumbnailFile.FileName);
                artwork.ThumbnailName = fileName = "artwork-" + artwork.Id + extension;
                string path = Path.Combine(wwwRootPath + "/img/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await artwork.ThumbnailFile.CopyToAsync(fileStream);
                }
                _context.Add(artwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artwork == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork.FindAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ThumbnailName")] Artwork artwork)
        {
            if (id != artwork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkExists(artwork.Id))
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
            return View(artwork);
        }

        // GET: Artworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artwork == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artwork == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Artwork'  is null.");
            }
            var artwork = await _context.Artwork.FindAsync(id);
            if (artwork != null)
            {
                _context.Artwork.Remove(artwork);
                if (artwork.ThumbnailName != null)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", artwork.ThumbnailName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkExists(int id)
        {
          return (_context.Artwork?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

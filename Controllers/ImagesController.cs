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
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImagesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Images
        public async Task<IActionResult> Index(int ExhibitId)
        {
            TempData["ExhibitId"] = ExhibitId;
            return _context.Image.Where(i => i.Exhibit.Id == ExhibitId) != null ? 
                      View(await _context.Image.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.Image'  is null.");
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Exhibit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create(int ExhibitId)
        {
            ViewData["ExhibitId"] = ExhibitId;
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Position,ImageFile")] Image image)
        {
            int ExhibitId = Convert.ToInt32(TempData["ExhibitId"]);
            Exhibit targetExhibit = _context.Exhibit.FirstOrDefault(e => e.Id == ExhibitId);
            image.Exhibit = targetExhibit;
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                image.ImageName = fileName = "image-" + ExhibitId + "-" + image.Position + extension;
                string path = Path.Combine(wwwRootPath + "/img/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(image);
                targetExhibit.Images.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.Include(i => i.Exhibit).FirstOrDefaultAsync(i => i.Id == id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Position,ImageName")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Exhibit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Image == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Image'  is null.");
            }
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                _context.Image.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

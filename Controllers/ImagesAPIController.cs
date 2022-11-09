using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using farris_art_gallery.Data;
using farris_art_gallery.Models;
using NuGet.Protocol;

namespace farris_art_gallery
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImage()
        {
          if (_context.Image == null)
          {
              return NotFound();
          }
            return await _context.Image.ToListAsync();
        }

        // GET: api/Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
          if (_context.Image == null)
          {
              return NotFound();
          }
            var image = await _context.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image.ToJson());
        }
    }
}

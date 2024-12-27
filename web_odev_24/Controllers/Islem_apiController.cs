using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Islem_apiController : ControllerBase
    {
        private readonly BerberContext _context;

        public Islem_apiController(BerberContext context)
        {
            _context = context;
        }

        // GET: api/Islem_api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Islem>>> GetIslemler()
        {
            return await _context.Islemler.ToListAsync();
        }

        // GET: api/Islem_api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Islem>> GetIslem(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);

            if (islem == null)
            {
                return NotFound();
            }

            return islem;
        }

        // PUT: api/Islem_api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIslem(int id, Islem islem)
        {
            if (id != islem.islemID)
            {
                return BadRequest();
            }

            _context.Entry(islem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IslemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Islem_api
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Islem>> PostIslem(Islem islem)
        {
            _context.Islemler.Add(islem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIslem", new { id = islem.islemID }, islem);
        }

        // DELETE: api/Islem_api/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIslem(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }

            _context.Islemler.Remove(islem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IslemExists(int id)
        {
            return _context.Islemler.Any(e => e.islemID == id);
        }
    }
}

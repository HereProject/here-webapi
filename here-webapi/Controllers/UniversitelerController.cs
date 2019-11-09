using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Data;
using here_webapi.Models.Kurumlar;

namespace here_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitelerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UniversitelerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Universiteler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Universite>>> GetUniversiteler()
        {
            return await _context.Universiteler.ToListAsync();
        }

        // GET: api/Universiteler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Universite>> GetUniversite(int id)
        {
            Universite uni = await _context.Universiteler.Include(x => x.Fakulteler)
                                                         .FirstOrDefaultAsync(x => x.Id == id);
            if (uni == null)
                return NotFound();

            return uni;
        }

        // PUT: api/Universiteler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUniversite(int id, Universite universite)
        {
            if (id != universite.Id)
            {
                return BadRequest();
            }

            _context.Entry(universite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversiteExists(id))
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

        // POST: api/Universiteler
        [HttpPost]
        public async Task<ActionResult<Universite>> PostUniversite(Universite universite)
        {
            _context.Universiteler.Add(universite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUniversite", new { id = universite.Id }, universite);
        }

        // DELETE: api/Universiteler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Universite>> DeleteUniversite(int id)
        {
            var universite = await _context.Universiteler.FindAsync(id);
            if (universite == null)
            {
                return NotFound();
            }

            _context.Universiteler.Remove(universite);
            await _context.SaveChangesAsync();

            return universite;
        }

        private bool UniversiteExists(int id)
        {
            return _context.Universiteler.Any(e => e.Id == id);
        }
    }
}

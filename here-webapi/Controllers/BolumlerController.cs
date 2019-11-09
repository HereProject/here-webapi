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
    public class BolumlerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BolumlerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bolumler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bolum>>> GetBolumler()
        {
            return await _context.Bolumler.ToListAsync();
        }

        // GET: api/Bolumler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bolum>> GetBolum(int id)
        {
            var bolum = await _context.Bolumler.FindAsync(id);

            if (bolum == null)
            {
                return NotFound();
            }

            return bolum;
        }

        // PUT: api/Bolumler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBolum(int id, Bolum bolum)
        {
            if (id != bolum.Id)
            {
                return BadRequest();
            }

            _context.Entry(bolum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BolumExists(id))
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

        // POST: api/Bolumler
        [HttpPost]
        public async Task<ActionResult<Bolum>> PostBolum(Bolum bolum)
        {
            _context.Bolumler.Add(bolum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBolum", new { id = bolum.Id }, bolum);
        }

        // DELETE: api/Bolumler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bolum>> DeleteBolum(int id)
        {
            var bolum = await _context.Bolumler.FindAsync(id);
            if (bolum == null)
            {
                return NotFound();
            }

            _context.Bolumler.Remove(bolum);
            await _context.SaveChangesAsync();

            return bolum;
        }

        private bool BolumExists(int id)
        {
            return _context.Bolumler.Any(e => e.Id == id);
        }
    }
}

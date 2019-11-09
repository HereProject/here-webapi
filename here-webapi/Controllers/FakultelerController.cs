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
    public class FakultelerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FakultelerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fakulteler?uniId=5
        [HttpGet("{uniId}")]
        public async Task<ActionResult<IEnumerable<Fakulte>>> GetFakulteler(int uniId)
        {
            Universite uni = await _context.Universiteler.Include(x => x.Fakulteler).FirstOrDefaultAsync(x => x.Id == uniId);

            if (uni == null)
                return NotFound();

            return uni.Fakulteler;
        }

        // GET: api/Fakulteler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fakulte>> GetFakulte(int id)
        {
            Fakulte fak = await _context.Fakulteler.Include(x => x.Bolumler).FirstOrDefaultAsync(x => x.Id == id);

            if (fak == null)
                return NotFound();

            return fak;
        }

        // POST: api/Fakulteler
        [HttpPost]
        public async Task<ActionResult<Fakulte>> PostFakulte(Fakulte fakulte)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (fakulte.UniversiteId <= 0)
                return BadRequest();

            if (!_context.Universiteler.Any(x => x.Id == fakulte.UniversiteId))
                return BadRequest();

            _context.Fakulteler.Add(fakulte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFakulte", new { id = fakulte.Id }, fakulte);
        }

        private bool FakulteExists(int id)
        {
            return _context.Fakulteler.Any(e => e.Id == id);
        }
    }
}

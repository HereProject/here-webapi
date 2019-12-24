using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Data;
using here_webapi.Models.Kurumlar;

namespace here_webapi.Controllers.V1.Kurumlar
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

        [HttpPost]
        public async Task<ActionResult<Bolum>> PostBolum(string BolumAd, int FakulteId)
        {
            if (FakulteId <= 0 || BolumAd == null)
                return BadRequest();

            Fakulte fak = await _context.Fakulteler.Include(x => x.Bolumler)
                                                   .FirstOrDefaultAsync(x => x.Id == FakulteId);

            if (fak == null)
                return BadRequest();

            if (fak.Bolumler.Any(x => x.Ad == BolumAd))
                return fak.Bolumler.FirstOrDefault(x => x.Ad == BolumAd);
            Bolum newBol = new Bolum()
            {
                FakulteId = fak.Id,
                Ad = BolumAd
            };
            _context.Bolumler.Add(newBol);

            await _context.SaveChangesAsync();


            return CreatedAtAction("GetBolum", new { id = newBol.Id }, newBol);
        }

    }
}

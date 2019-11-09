using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Data;
using here_webapi.Models.Kurumlar;
using here_webapi.Contracts.V1;

namespace here_webapi.Controllers.V1
{
    [ApiController]
    public class UniversitelerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UniversitelerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoute.Universiteler.GetAll)]
        public async Task<ActionResult<IEnumerable<Universite>>> GetAll()
        {
            return await _context.Universiteler.ToListAsync();
        }

        [HttpGet(ApiRoute.Universiteler.Get)]
        public async Task<ActionResult<Universite>> Get(int id)
        {
            Universite uni = await _context.Universiteler.Include(x => x.Fakulteler)
                                                         .FirstOrDefaultAsync(x => x.Id == id);
            if (uni == null)
                return NotFound();

            return uni;
        }

        [HttpPost()]
        public async Task<ActionResult<Universite>> Create(Universite universite)
        {


            return CreatedAtAction(nameof(Get), new { id = universite.Id }, universite);
        }
    }
}

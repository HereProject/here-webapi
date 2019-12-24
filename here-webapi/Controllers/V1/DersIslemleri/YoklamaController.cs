using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Data;
using here_webapi.Models.Yoklama;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using here_webapi.Models.DersModels;

namespace here_webapi.Controllers.V1.DersIslemleri
{
    [ApiController]
    public class YoklamaController : HEREController
    {
        public YoklamaController(AppDbContext context, UserManager<AppUser> userManager) : base(context, userManager)
        {
        }

        [HttpPost("api/v1/yoklama")]
        [Authorize(Roles = "Öğrenci")]
        public async Task<ActionResult> BeniYokla(string Key)
        {
            List<Ders> ogrenciDersleri = await _context.AlinanDersler.Where(x => x.OgrenciId == ActiveUserId)
                                                                     .Include(x => x.Ders)
                                                                     .Select(x => x.Ders)
                                                                     .ToListAsync();

            AcilanDers ders = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.Key == Key && x.SonGecerlilik >= DateTime.Now && ogrenciDersleri.Select(oD => oD.Id).Contains(x.Id));

            if (ders == null)
                return BadRequest();

            
            if (await _context.YoklananOgrenciler.FirstOrDefaultAsync(x => x.OgrenciId == ActiveUserId && x.Key == Key) != null)
                return Ok();

            YoklananOgrenci yoklananOgrenci = new YoklananOgrenci()
            {
                DersId = ders.DersId,
                OgrenciId = ActiveUserId,
                Key = Key
            };

            _context.YoklananOgrenciler.Add(yoklananOgrenci);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}

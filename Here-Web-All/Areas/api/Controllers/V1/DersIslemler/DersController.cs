using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Models.DersModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using here_webapi.Models.Identity;
using here_webapi.Models.Yoklama;
using Here_Web_All.Data;

namespace here_webapi.Controllers.V1.DersIslemleri
{
    [ApiController]
    [Authorize]
    public class DersController : HEREController
    {
        public DersController(AppDbContext context, UserManager<AppUser> userManager): base(context, userManager)
        {
        }

        [HttpGet("api/v1/derslerim")]
        public async Task<ActionResult<List<Ders>>> Derslerim()
        {
            List<Ders> dersler = await _context.AlinanDersler.Where(x => x.OgrenciId == ActiveUserId)
                                                             .Include(x => x.Ders)
                                                             .Select(x => x.Ders)
                                                             .ToListAsync();

            return dersler;
        }

        [HttpPost("api/v1/dersekle")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult<Ders>> DersEkle(string DersAdi)
        {
            AppUser user = await ActiveUser;
            Ders ders = new Ders() { BolumId = (int)user.BolumId, OgretmenId = user.Id, DersAdi = DersAdi };
            _context.Dersler.Add(ders);
            await _context.SaveChangesAsync();

            return ders;
        }

        [HttpPost("api/v1/derse-ogrenci-ekle")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult> DerseOgrenciEkle(List<int> ogrenciNo, int dersNo)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == dersNo && x.OgretmenId == ActiveUserId);
            if (ders == null)
                return BadRequest();

            List<AlinanDers> alinanDersler = await _context.AlinanDersler.Where(x => x.DersId == dersNo)
                                                                         .Include(x => x.Ders)
                                                                         .ToListAsync();

            ogrenciNo.RemoveAll(x => alinanDersler.Select(aD => aD.OgrenciId).Contains(x));

            foreach (int ogrNo in ogrenciNo)
            {
                _context.AlinanDersler.Add(new AlinanDers() { OgrenciId = ogrNo, DersId = dersNo });
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("api/v1/ders-yoklama-ac")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult<AcilanDers>> DersYoklamaAc(int DersId, int Dakika)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == DersId && x.OgretmenId == ActiveUserId);

            if (ders == null)
                return BadRequest();
            
            AcilanDers acilanDers = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.DersId == DersId && x.SonGecerlilik >= DateTime.Now);

            if (acilanDers != null)
                return acilanDers;

            AcilanDers yoklamaAc = new AcilanDers()
            {
                DersId = ders.Id,
                SonGecerlilik = DateTime.Now.AddMinutes(Dakika),
                Key = Guid.NewGuid().ToString()
            };

            _context.AcilanDersler.Add(yoklamaAc);
            await _context.SaveChangesAsync();

            return yoklamaAc;
        }

        [HttpPost("api/v1/aktif-yoklama")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult<AcilanDers>> AktifYoklama(int DersId)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == DersId && x.OgretmenId == ActiveUserId);
            if (ders == null)
                return BadRequest();

            AcilanDers yoklama = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.DersId == DersId && x.SonGecerlilik >= DateTime.Now);
            if (yoklama == null)
                return BadRequest();

            return yoklama;
        }

    }
}

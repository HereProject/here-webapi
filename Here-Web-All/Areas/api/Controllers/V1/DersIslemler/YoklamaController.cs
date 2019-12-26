using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Models.Yoklama;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using here_webapi.Models.DersModels;
using here_webapi.Contracts.V1.Responses.DersIslemleri;
using Here_Web_All.Data;
using here_webapi.Services;

namespace here_webapi.Controllers.V1.DersIslemleri
{
    [ApiController]
    public class YoklamaController : HEREController
    {
        private readonly IdentityService _identityService;
        public YoklamaController(AppDbContext context, UserManager<AppUser> userManager, IdentityService iSer) : base(context, userManager)
        {
            _identityService = iSer;
        }

        [HttpPost("api/v1/yoklama")]
        [Authorize(Roles = "Öğrenci")]
        public async Task<ActionResult> BeniYokla(string Key, string eposta, string sifre)
        {

            var log = await _identityService.LoginAsync(eposta, sifre);

            if (!log.Success)
                return Unauthorized();

            AppUser user = await _userManager.FindByEmailAsync(eposta);

            List<Ders> ogrenciDersleri = await _context.AlinanDersler.Where(x => x.OgrenciId == user.Id)
                                                                     .Include(x => x.Ders)
                                                                     .Select(x => x.Ders)
                                                                     .ToListAsync();
            DateTime now = DateTime.Now;
            AcilanDers ders = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.Key == Key && x.SonGecerlilik >= now && ogrenciDersleri.Select(oD => oD.Id).Contains(x.Id));

            if (ders == null)
                return BadRequest();

            
            if (await _context.YoklananOgrenciler.FirstOrDefaultAsync(x => x.OgrenciId == user.Id && x.Key == Key) != null)
                return Ok();

            YoklananOgrenci yoklananOgrenci = new YoklananOgrenci()
            {
                DersId = ders.DersId,
                OgrenciId = user.Id,
                Key = Key
            };

            _context.YoklananOgrenciler.Add(yoklananOgrenci);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("api/v1/yoklama-bilgisi")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult<List<DersYoklamaBilgisi>>> DersYoklamaBilgisi(int DersKod)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == DersKod && x.OgretmenId == ActiveUserId);
            if (ders == null)
                return BadRequest();
            List<YoklananOgrenci> yoklananlar = await _context.YoklananOgrenciler.Where(x => x.DersId == DersKod).ToListAsync();
            List<AppUser> dersOgrencileri = await _context.AlinanDersler.Where(x => x.Id == DersKod)
                                                                        .Include(x => x.Ogrenci)
                                                                            .ThenInclude(x => x.OgrenciDetay)
                                                                        .Select(x => x.Ogrenci)
                                                                        .ToListAsync();

            List<DersYoklamaBilgisi> dersYoklamaBilgisi = new List<DersYoklamaBilgisi>();
            List<int> yoklananIdLer = yoklananlar.Select(x => x.OgrenciId).ToList();
            foreach (AppUser appUser in dersOgrencileri)
            {
                dersYoklamaBilgisi.Add(new DersYoklamaBilgisi() { Ogrenci = appUser, Yoklandi = yoklananIdLer.Contains(appUser.Id) });
            }
            return dersYoklamaBilgisi;
        }

        [HttpPost("api/v1/yoklama-uzat")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult> YoklamaUzat(int YoklamaID, int PlusMins)
        {
            List<int> hocaDersleri = await _context.Dersler.Where(x => x.OgretmenId == ActiveUserId).Select(x => x.Id).ToListAsync();
            AcilanDers yoklama = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.Id == YoklamaID && hocaDersleri.Contains(x.DersId));
            if (yoklama == null)
                return BadRequest();

            if (yoklama.SonGecerlilik <= DateTime.Now)
                yoklama.SonGecerlilik = DateTime.Now.AddMinutes(PlusMins);
            else
                yoklama.SonGecerlilik.AddMinutes(PlusMins);

            _context.Update(yoklama);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("api/v1/yoklama-ogrenci-ekle-kaldir")]
        [Authorize(Roles = "Öğretmen")]
        public async Task<ActionResult> OgrenciEkleKaldir(int YoklamaID, int OgrenciID, bool Ekle)
        {
            List<int> hocaDersleri = await _context.Dersler.Where(x => x.OgretmenId == ActiveUserId).Select(x => x.Id).ToListAsync();
            AcilanDers yoklama = await _context.AcilanDersler.FirstOrDefaultAsync(x => x.Id == YoklamaID && hocaDersleri.Contains(x.DersId));
            if (yoklama == null)
                return BadRequest();

            AppUser ogrenci = await _context.Users.Include(x => x.AlinanDersler).ThenInclude(d => d.Ders).FirstOrDefaultAsync(x => x.Id == OgrenciID);
            if (ogrenci == null || ogrenci.AlinanDersler == null || !ogrenci.AlinanDersler.Select(x => x.Id).Contains(yoklama.DersId))
                return BadRequest();

            YoklananOgrenci yoklananOgrenci = await _context.YoklananOgrenciler.FirstOrDefaultAsync(x => x.Key == yoklama.Key && x.DersId == yoklama.DersId && x.OgrenciId == OgrenciID);
            if(yoklananOgrenci == null)
            {
                if (!Ekle)
                    return Ok();
                YoklananOgrenci yokOg = new YoklananOgrenci()
                {
                    DersId = yoklama.DersId,
                    Key = yoklama.Key,
                    OgrenciId = OgrenciID
                };
                _context.YoklananOgrenciler.Add(yokOg);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                if (Ekle)
                    return Ok();

                _context.Remove(yoklananOgrenci);
                await _context.SaveChangesAsync();
                return Ok();
            }
        
        }

    }
}
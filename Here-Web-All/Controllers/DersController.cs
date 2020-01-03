using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Here_Web_All.Data;
using here_webapi.Contracts.V1.Responses.DersIslemleri;
using here_webapi.Controllers.V1;
using here_webapi.Models.DersModels;
using here_webapi.Models.Identity;
using here_webapi.Models.Yoklama;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Here_Web_All.Controllers
{
    [Authorize(Roles = "Öğretmen")]
    public class DersController : HERE_WebController
    {
        public DersController(AppDbContext context, UserManager<AppUser> userManager) : base(context, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Ders> dersler = await _context.Dersler.Where(x => x.OgretmenId == ActiveUserId).ToListAsync();

            return View(dersler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == id && x.OgretmenId == ActiveUserId);

            if (ders == null)
                return RedirectToAction(nameof(Index));

            List<AppUser> dersiAlanlar = (await _context.AlinanDersler.Where(x => x.DersId == ders.Id)
                                                                      .Include(x => x.Ogrenci)
                                                                        .ThenInclude(x => x.OgrenciDetay).ToListAsync()).Select(x => x.Ogrenci).ToList();
            ViewData["ders_ogrenciler"] = dersiAlanlar;

            List<int> ids = await _context.UserRoles.Where(x => x.RoleId == 4).Select(x => x.UserId).ToListAsync();
            ViewData["ogrenciler"] = await _context.Users.Where(x => ids.Contains(x.Id) && !dersiAlanlar.Select(yx => yx.Id).Contains(x.Id)).Include(x => x.OgrenciDetay).ToListAsync();

            ViewData["yoklamalar"] = await _context.AcilanDersler.Where(x => x.DersId == ders.Id).ToListAsync();

            return View(ders);
        }

        [HttpPost]
        [Route("/ders/ogrenci-ekle")]
        public async Task<IActionResult> DerseOgrenciEkle(int dersNo, int OgrId)
        {
            Ders ders = await _context.Dersler.FirstOrDefaultAsync(x => x.Id == dersNo && x.OgretmenId == ActiveUserId);
            if (ders == null)
                return BadRequest();

            AlinanDers ders2 = await _context.AlinanDersler.FirstOrDefaultAsync(x => x.DersId == dersNo && x.OgrenciId == OgrId);
            if (ders2 != null)
                return RedirectToAction(nameof(Edit), new { id = dersNo });
                
            _context.AlinanDersler.Add(new AlinanDers() { DersId = dersNo, OgrenciId = OgrId });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = dersNo });
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string dersadi)
        {
            int bolumId = (int)ActiveUser.Result.BolumId;
            Ders ders = new Ders() { OgretmenId = ActiveUserId, DersAdi = dersadi, BolumId = bolumId };
            _context.Dersler.Add(ders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = ders.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Yoklama(int id)
        {
            List<int> dersIdler = await _context.Dersler.Where(x => x.OgretmenId == ActiveUserId).Select(x => x.Id).ToListAsync();
            DateTime date = DateTime.Now;
            AcilanDers ders = await _context.AcilanDersler.Where(x => x.Id == id && dersIdler.Contains(x.DersId))
                                                          .Include(x => x.Ders)
                                                          .FirstOrDefaultAsync();

            if (ders == null)
                return RedirectToAction(nameof(Index));


            return View(ders);
        }

        [HttpPost]
        [Route("ders/yoklama-ac")]
        public async Task<IActionResult> YoklamaAc(int DersId, int Dakika)
        {
            List<int> dersIdler = (await _context.Dersler.Where(x => x.OgretmenId == ActiveUserId).ToListAsync()).Select(x => x.Id).ToList();
            AcilanDers ders = await _context.AcilanDersler.Where(x => x.DersId == DersId && dersIdler.Contains(x.DersId) && x.SonGecerlilik >= DateTime.Now).FirstOrDefaultAsync();
            
            
            Dakika = Math.Abs(Dakika);

            if(ders != null)
            {
                ders.SonGecerlilik.AddMinutes(Dakika);
                _context.Update(ders);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Yoklama), new { id = ders.Id });
            }

            AcilanDers yoklama = new AcilanDers()
            {
                DersId = DersId,
                SonGecerlilik = DateTime.Now.AddMinutes(Dakika),
                Key = Guid.NewGuid().ToString()
            };

            _context.AcilanDersler.Add(yoklama);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Yoklama), new { id = yoklama.Id});
        }

        [HttpGet]
        [Route("ders/yoklama/gelenler")]
        public async Task<IActionResult> YoklamaGelenler(int yokId)
        {
            AcilanDers yoklama = await _context.AcilanDersler.Include(x => x.Ders).FirstOrDefaultAsync(x => x.Id == yokId);

            if (yoklama == null)
                return BadRequest();

            if (yoklama.Ders.OgretmenId != ActiveUserId)
                return BadRequest();

            List<int> ogrIds = (await _context.AlinanDersler.Where(x => x.DersId == yoklama.Ders.Id).ToListAsync()).Select(x => x.OgrenciId).ToList();
            List<AppUser> dersOgrencileri = await _context.Users.Where(x => ogrIds.Contains(x.Id)).Include(x => x.OgrenciDetay).ToListAsync();
            List<int> yoklananIdler = (await _context.YoklananOgrenciler.Where(x => x.DersId == yoklama.DersId && x.Key == yoklama.Key).ToListAsync()).Select(x => x.OgrenciId).ToList();


            List<DersYoklamaBilgisi> dersYoklamaBilgisi = new List<DersYoklamaBilgisi>();
            foreach (AppUser appUser in dersOgrencileri)
            {
                dersYoklamaBilgisi.Add(new DersYoklamaBilgisi() { Ogrenci = appUser, Yoklandi = yoklananIdler.Contains(appUser.Id) });
            }

            return Json(dersYoklamaBilgisi);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Here_Web_All.Data;
using Here_Web_All.InputModels;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Here_Web_All.Controllers
{
    [Authorize(Roles = "Öğretmen")]
    public class OgrenciController : HERE_WebController
    {
        public OgrenciController(AppDbContext context, UserManager<AppUser> userManager) : base(context, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppUserRole> roller = await _context.UserRoles.Where(x => x.RoleId == 4).ToListAsync();
            List<AppUser> ogrenciler = await _context.Users.Where(x => roller.Select(y => y.UserId).Contains(x.Id)).Include(x => x.OgrenciDetay).ToListAsync();
            return View(ogrenciler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OgrenciInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            AppUser newUser = new AppUser() { Email = input.Eposta, UserName = input.Eposta };
            newUser.UniversiteId = 1;
            newUser.FakulteId = 1;
            newUser.BolumId = 1;
            IdentityResult x = await _userManager.CreateAsync(newUser, input.Sifre);
            if (!x.Succeeded)
            {
                ModelState.AddModelError("err", "Hata oluştu");
            }
            await _userManager.AddToRoleAsync(newUser, "Öğrenci");
            
            OgrenciDetay detay = new OgrenciDetay()
            {
                Gender = input.Cinsiyet == 0 ? Gender.Erkek : Gender.Kadin,
                TC = "12345678912",
                Ad = input.Ad,
                Soyad = input.SoyAd,
                UserId = newUser.Id,
            };
            _context.OgrenciDetaylari.Add(detay);
            await _context.SaveChangesAsync();
            
            return View();
        }
    }
}
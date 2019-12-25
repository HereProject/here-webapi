using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Here_Web_All.Data;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Here_Web_All.Controllers
{
    public class HERE_WebController : Controller
    {
        protected readonly AppDbContext _context;
        protected readonly UserManager<AppUser> _userManager;
        protected int ActiveUserId => int.Parse(_userManager.GetUserId(this.User));
        protected Task<AppUser> ActiveUser
        {
            get
            {
                return _userManager.GetUserAsync(this.User);
            }
        }

        public HERE_WebController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}
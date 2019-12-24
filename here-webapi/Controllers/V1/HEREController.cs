using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using here_webapi.Data;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace here_webapi.Controllers.V1
{
    public class HEREController : ControllerBase
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

        public HEREController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}
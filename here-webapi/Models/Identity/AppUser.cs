using here_webapi.Models._Defs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Identity
{
    public class AppUser : IdentityUser<int>
    {

        public AppUser() : base()
        {

        }

        public AppUser(string userName) : base(userName)
        {

        }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<AppUserClaim> Claims { get; set; }

        public virtual ICollection<AppUserLogin> Logins { get; set; }

        public virtual ICollection<AppUserToken> Tokens { get; set; }
    }

    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName) { this.NormalizedName = roleName.ToUpper().Normalize(); }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }

    }

    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<int>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<int>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppRoleClaim : IdentityRoleClaim<int>
    {
        public virtual AppRole Role { get; set; }
    }

    public class AppUserToken : IdentityUserToken<int>
    {
        public virtual AppUser User { get; set; }
    }
}

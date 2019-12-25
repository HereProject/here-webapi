using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1.Requests.Identity
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Eposta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli Eposta adresi giriniz.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Şifre girişi zorunludur.")]
        public string Password { get; set; }
    }
}

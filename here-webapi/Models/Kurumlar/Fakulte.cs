using here_webapi.Models._Defs;
using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Kurumlar
{
    public class Fakulte : DbObject
    {
        [Required(ErrorMessage = "Fakülte Adı Girilmelidir.")]
        [MaxLength(255, ErrorMessage = "Fakülte Adı 255 Karakteri Geçemez.")]
        public string Ad { get; set; }

        public int UniversiteId { get; set; }
        public Universite Universite { get; set; }

        public List<Bolum> Bolumler { get; set; }

        public List<AppUser> Kisiler { get; set; }
    }
}

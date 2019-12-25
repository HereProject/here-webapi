using here_webapi.Models._Defs;
using here_webapi.Models.DersModels;
using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Kurumlar
{
    public class Bolum : DbObject
    {
        [Required(ErrorMessage = "Bölüm Adı Girilmelidir.")]
        [MaxLength(255, ErrorMessage = "Bölüm Adı 255 Karakteri Geçemez.")]
        public string Ad { get; set; }

        public int FakulteId { get; set; }
        public Fakulte Fakulte { get; set; }


        public List<Ders> Dersler { get; set; }
        public List<AppUser> Kisiler { get; set; }
    }
}

using here_webapi.Models._Defs;
using here_webapi.Models.Identity;
using here_webapi.Models.Kurumlar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.DersModeller
{
    public class Ders : DbObject
    {
        public int BolumId { get; set; }
        public Bolum Bolum { get; set; }

        [Required]
        [MaxLength(50)]
        public string DersKodu { get; set; }

        [Required]
        [MaxLength(100)]
        public string DersAdi { get; set; }

        public int OgretmenId { get; set; }
        public AppUser Ogretmen { get; set; }

        public List<AlinanDersler> DersiAlanlar { get; set; }
    }
}

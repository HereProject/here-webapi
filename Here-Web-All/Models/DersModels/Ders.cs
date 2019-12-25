using here_webapi.Models._Defs;
using here_webapi.Models.Identity;
using here_webapi.Models.Kurumlar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.DersModels
{
    public class Ders : DbObject
    {
        [Required]
        [MaxLength(255)]
        public string DersAdi { get; set; }
        
        public int BolumId { get; set; }
        public Bolum Bolum { get; set; }

        public int OgretmenId { get; set; }
        public AppUser Ogretmen { get; set; }

    }
}

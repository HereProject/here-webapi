using here_webapi.Models._Defs;
using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.DersModeller
{
    public class AlinanDersler : DbObject
    {
        public int OgrenciId { get; set; }
        public AppUser Ogrenci { get; set; }

        public int DersId { get; set; }
        public Ders Ders { get; set; }

    }
}

using here_webapi.Models._Defs;
using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.DersModels
{
    public class AlinanDers : DbObject
    {
        public int DersId { get; set; }
        public Ders Ders { get; set; }

        public int OgrenciId { get; set; }
        public AppUser Ogrenci { get; set; }
    }
}

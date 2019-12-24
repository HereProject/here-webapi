using here_webapi.Models._Defs;
using here_webapi.Models.DersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Yoklama
{
    public class AcilanDers : DbObject
    {
        public int DersId { get; set; }
        public Ders Ders { get; set; }

        public DateTime SonGecerlilik { get; set; }

        public string Key { get; set; }

    }
}

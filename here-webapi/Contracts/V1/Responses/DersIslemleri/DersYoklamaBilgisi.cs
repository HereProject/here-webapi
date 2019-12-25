using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1.Responses.DersIslemleri
{
    public class DersYoklamaBilgisi
    {
        public AppUser Ogrenci { get; set; }
        public bool Yoklandi { get; set; }
    }
}

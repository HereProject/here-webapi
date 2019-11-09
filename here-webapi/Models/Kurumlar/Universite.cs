using here_webapi.Models._Defs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Kurumlar
{
    public class Universite : DbObject
    {
        [Required(ErrorMessage = "Universite Adı Girilmelidir.")]
        [MaxLength(255, ErrorMessage = "Universite Adı 255 Karakteri Geçemez.")]
        public string Ad { get; set; }

        public List<Fakulte> Fakulteler { get; set; }

    }
}

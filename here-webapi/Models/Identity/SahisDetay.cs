using here_webapi.Models._Defs;
using here_webapi.Models.Kurumlar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Identity
{
    public enum Gender : byte
    {
        Erkek,
        Kadin
    }

    public class SahisDetay : DbObject
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        
        [MaxLength(100)]
        public string Ad { get; set; }

        [MaxLength(100)]
        public string Soyad { get; set; }

        [RegularExpression(@"^[1-9]\d{10}$", ErrorMessage = "Lütfen 11 haneli T.C. Kimlik Numaranızı Giriniz.")]
        [MaxLength(11, ErrorMessage = "Lütfen 11 haneli T.C. Kimlik Numaranızı Giriniz.")]
        [MinLength(11, ErrorMessage = "Lütfen 11 haneli T.C. Kimlik Numaranızı Giriniz.")]
        public string TC { get; set; }

        [Column("Gender")]
        public bool _Gender { get; set; }

        [NotMapped]
        public Gender Gender
        {
            get
            {
                if (_Gender == false)
                    return Gender.Erkek;
                else
                    return Gender.Kadin;
            }
            set
            {
                if (value == Gender.Erkek)
                    _Gender = false;
                else
                    _Gender = true;
            }
        }

    }
}
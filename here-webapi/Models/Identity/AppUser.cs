using here_webapi.Models._Defs;
using here_webapi.Models.DersModeller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models.Identity
{
    public enum UserType : byte
    {
        Ogrenci = 0,
        OGorevli = 1
    }

    public class AppUser : DbObject
    {
        [Required]
        [MaxLength(50)]
        public string SicilNo { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Column("Type")]
        public byte _UserType { get; set; }

        [NotMapped]
        public UserType UserType
        {
            get => (UserType)_UserType;
            set => _UserType = (byte)value;
        }

        public List<Ders> VerilenDersler { get; set; }
        
        public List<AlinanDersler> AlinanDersler { get; set; }

    }
}

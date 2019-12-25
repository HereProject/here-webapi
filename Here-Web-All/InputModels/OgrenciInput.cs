using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Here_Web_All.InputModels
{
    public class OgrenciInput
    {
        [Required]
        [EmailAddress]
        public string Eposta { get; set; }
    
        [Required]
        public string Sifre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ad { get; set; }

        [Required]
        [MaxLength(50)]
        public string SoyAd { get; set; }

        [Required]
        public byte Cinsiyet { get; set; }
    }
}

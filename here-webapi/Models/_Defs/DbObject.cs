using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Models._Defs
{
    public class DbObject
    {
        [Key]
        public int Id { get; set; }
    }
}

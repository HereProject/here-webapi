using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1.Requests.Identity.Role
{
    public class GenerateRoleRequest
    {
        [Required]
        [MaxLength(255)]
        public string RoleName { get; set; }
    }
}

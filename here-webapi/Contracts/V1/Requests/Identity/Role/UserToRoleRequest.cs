using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1.Requests.Identity.Role
{
    public class UserToRoleRequest
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

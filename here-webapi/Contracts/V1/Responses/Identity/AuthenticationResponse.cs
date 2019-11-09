using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1.Responses.Identity
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}

using here_webapi.Contracts.V1.Responses.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResponse> RegisterAsync(string email, string password);
        Task<AuthenticationResponse> LoginAsync(string email, string password);
    }
}

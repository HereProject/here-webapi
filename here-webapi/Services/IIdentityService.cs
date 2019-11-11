using here_webapi.Contracts.V1.Responses.Identity;
using here_webapi.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResponse> RegisterAsync(string email, string password, UserType userType, int? uniId = null, int? fakId = null, int? bolId = null);
        Task<AuthenticationResponse> LoginAsync(string email, string password);
    }
}

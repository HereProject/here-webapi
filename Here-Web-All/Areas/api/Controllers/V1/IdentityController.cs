using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using here_webapi.Contracts.V1;
using here_webapi.Contracts.V1.Requests;
using here_webapi.Contracts.V1.Requests.Identity;
using here_webapi.Contracts.V1.Responses.Identity;
using here_webapi.Models.Identity;
using here_webapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace here_webapi.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoute.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage)
                });
            }

            AuthenticationResponse authResponse = await _identityService.RegisterAsync(request.Email, request.Password, UserType.Admin);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }
            else
            {
                return Ok(authResponse);
            }

        }

        [HttpPost(ApiRoute.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage)
                });
            }

            AuthenticationResponse authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }
            else
            {
                return Ok(authResponse);
            }
        }

    }
}
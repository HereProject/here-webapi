using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using here_webapi.Contracts.V1.Responses.Identity;
using here_webapi.Models.Identity;
using here_webapi.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace here_webapi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResponse> LoginAsync(string email, string password)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = new string[] { "Kullanıcı kayıtlı değil." }
                };
            }

            bool checkPass = await _userManager.CheckPasswordAsync(user, password);

            if (!checkPass)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = new string[] { "Eposta adresi ile şifre uyuşmuyor." }
                };
            }

            return GenerateTokenForUsers(user);
        }

        public async Task<AuthenticationResponse> RegisterAsync(string email, string password)
        {
            if((await _userManager.FindByEmailAsync(email)) != null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = new string[] { "Eposta adresi sistemde kayıtlı" },
                };
            }
            AppUser tempUser = new AppUser(email);
            tempUser.Email = tempUser.UserName;
            IdentityResult newUser = await _userManager.CreateAsync(tempUser, password);

            if (!newUser.Succeeded)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = newUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateTokenForUsers(tempUser);
        }

        private AuthenticationResponse GenerateTokenForUsers(AppUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResponse
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
            };
        }
    }
}

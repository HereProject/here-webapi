using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Here_Web_All.Data;
using here_webapi.Contracts.V1.Responses.Identity;
using here_webapi.Models.Identity;
using here_webapi.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace here_webapi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public IdentityService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> LoginAsync(string email, string password)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new string[] { "Kullanıcı kayıtlı değil." }
                };
            }

            bool checkPass = await _userManager.CheckPasswordAsync(user, password);

            if (!checkPass)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new string[] { "Eposta adresi ile şifre uyuşmuyor." }
                };
            }

            return GenerateTokenForUsers(user);
        }

        public async Task<AuthenticationResponse> RegisterAsync(string email, string password, UserType userType, int? uniId = null, int? fakId = null, int? bolId = null)
        {
            if ((await _userManager.FindByEmailAsync(email)) != null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new string[] { "Eposta adresi sistemde kayıtlı" },
                };
            }

            if (userType != UserType.Admin && (uniId == null || fakId == null || bolId == null))
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new string[] { "Kurum tanımlaması yanlış!" },
                };
            }

            AppUser tempUser = new AppUser(email);
            tempUser.Email = tempUser.UserName;

            tempUser.UniversiteId = uniId;
            tempUser.FakulteId = fakId;
            tempUser.BolumId = bolId;
            tempUser.UserType = userType;

            IdentityResult newUser = await _userManager.CreateAsync(tempUser, password);

            if (!newUser.Succeeded)
            {

                _context.UserRoles.Add(new AppUserRole() { UserId = tempUser.Id, RoleId = ((int)userType + 1) });
                await _context.SaveChangesAsync();

                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = newUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateTokenForUsers(tempUser);
        }

        private AuthenticationResponse GenerateTokenForUsers(AppUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("cok-gizli-bir-32-karakter-secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
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

            var token2 = new JwtSecurityToken
                        (
                            claims: new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                                new Claim("id", user.Id.ToString()),
                            },
                            expires: DateTime.UtcNow.AddHours(2), // 30 gün geçerli olacak
                            notBefore: DateTime.UtcNow,
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cok-gizli-bir-32-karakter-secret")),
                                    SecurityAlgorithms.HmacSha256)
                        );

            return new AuthenticationResponse
            {
                Success = true,
                //Token = tokenHandler.WriteToken(token),
                Token = new JwtSecurityTokenHandler().WriteToken(token2)
            };
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;

namespace Talabat.Service.Tokens
{
    public class TokenService : ITokenService
    {
        public IConfiguration _configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> _userManager)
        {
            // Private Claims [User Defined]

            var authClaims = new List<Claim>() {

            new Claim(ClaimTypes.GivenName , user.DisplayName) ,
            new Claim (ClaimTypes.Email , user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(

                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:Duration"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

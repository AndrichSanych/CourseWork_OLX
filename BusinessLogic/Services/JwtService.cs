using BusinessLogic.Interfaces;
using System.Text;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;



namespace BusinessLogic.Services
{
    internal class JWTService :IJwtService
    {
        private readonly UserManager<User> userManager;
        private readonly JwtOptions jwtOpts;

        public JWTService(IConfiguration configuration, UserManager<User> userManager)
        {
            this.userManager = userManager;
            jwtOpts = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
                ?? throw new HttpException("Invalid JWT setting", HttpStatusCode.InternalServerError);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.Name!),
                new (ClaimTypes.Surname, user.Surname!),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.DateOfBirth, user.Birthdate.ToShortDateString()),
                new (ClaimTypes.HomePhone, user.PhoneNumber ?? ""),
                new (ClaimTypes.Anonymous, user.Avatar ?? ""),
                new (ClaimTypes.UserData, user.RegisterDate.ToShortDateString()),
            };
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            return claims;
        }

        private SigningCredentials getCredentials(JwtOptions options)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var time = DateTime.UtcNow.AddMinutes(jwtOpts.AccessTokenLifetimeInMinutes);
            var credentials = getCredentials(jwtOpts);
            var token = new JwtSecurityToken(
                issuer: jwtOpts.Issuer,
                claims: claims,
                expires: time,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
    }
}

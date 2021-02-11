using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreLearning.DBLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoreLearning.Infrastructure.Business
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;

        public string CreateToken(string login)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(configuration[ "TokenAuthOptions:Issuer" ], configuration[ "TokenAuthOptions:Audience" ], notBefore: now, claims: GetIdentity(login).Claims,
                                           expires: now.Add(TimeSpan.FromMinutes(Convert.ToInt32(configuration[ "TokenAuthOptions:Lifetime" ]))),
                                           signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[ "TokenAuthOptions:Key" ])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static ClaimsIdentity GetIdentity(string login)
        {
            var claims = new List<Claim> {new Claim(ClaimsIdentity.DefaultNameClaimType, login)};
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
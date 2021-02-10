using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [AllowAnonymous]
    public class RegistrationAndLoginController : Controller
    {
        public RegistrationAndLoginController( UserRepository repository )
        {
            this.repository = repository;
        }

        private readonly UserRepository repository;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register( RegisterModel registerModel )
        {
            if ( !ModelState.IsValid )
                BadRequest();

            var identity = GetIdentity( registerModel.Email, registerModel.Password );

            if ( identity != null )
                return new BadRequestResult();

            var user = new User { AuthenticationData = new AuthenticationData { Login = registerModel.Email, Password = registerModel.Password } };
            repository.AddUser( user );

            return Json( registerModel );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login( LoginModel loginModel )
        {
            if ( !ModelState.IsValid )
                BadRequest();

            var identity = GetIdentity( loginModel.Email, loginModel.Password );

            if ( identity == null )
                return BadRequest();

            var encodedJwt = CreateToken( identity );
            var response = new { access_token = encodedJwt, username = identity.Name };

            return Ok( new { Token = Json( response ) } );
        }

        private string CreateToken( ClaimsIdentity identity )
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken( TokenAuthOptions.ISSUER, TokenAuthOptions.AUDIENCE, notBefore: now, claims: identity.Claims, expires: now.Add( TimeSpan.FromMinutes( TokenAuthOptions.LIFETIME ) ),
                                            signingCredentials: new SigningCredentials( TokenAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 ) );

            return new JwtSecurityTokenHandler().WriteToken( jwt );
        }

        private ClaimsIdentity GetIdentity( string username, string password )
        {
            var user = repository.GetUsers().FirstOrDefault( x => x.AuthenticationData.Login == username && x.AuthenticationData.Password == password );

            if ( user != null )
            {
                var claims = new List< Claim > { new Claim( ClaimsIdentity.DefaultNameClaimType, user.AuthenticationData.Login ) };
                var claimsIdentity = new ClaimsIdentity( claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType );

                return claimsIdentity;
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route( "{controller}/{action}" )]
    public class RegistrationAndLoginController : ControllerBase
    {
        public RegistrationAndLoginController( IUserRepository repository )
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Register( RegisterModel registerModel )
        {
            if ( !ModelState.IsValid )
                BadRequest();

            var identity = GetIdentity( registerModel.Email, registerModel.Password );

            if ( identity != null )
                return new BadRequestResult();

            var user = new User { Login = registerModel.Email, Password = registerModel.Password };
            repository.AddUser( user );
            repository.Save();

            return Ok(new { Message = "New user", Token = new JsonResult(registerModel) });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Login( LoginModel loginModel )
        {
            if ( !ModelState.IsValid )
                BadRequest();

            var identity = GetIdentity( loginModel.Email, loginModel.Password );

            if ( identity == null )
                return BadRequest();

            var encodedJwt = CreateToken( identity );
            var response = new { access_token = encodedJwt, username = identity.Name };

            return Ok( new { Token = new JsonResult( response ) } );
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
            var users = repository.GetUsers();
            var user = users.FirstOrDefault( x => x.Login == username && x.Password == password );

            if ( user != null )
            {
                var claims = new List< Claim > { new Claim( ClaimsIdentity.DefaultNameClaimType, user.Login ) };
                var claimsIdentity = new ClaimsIdentity( claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType );

                return claimsIdentity;
            }

            return null;
        }
    }
}
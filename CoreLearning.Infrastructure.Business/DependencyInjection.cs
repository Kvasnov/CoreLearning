using System.Reflection;
using System.Text;
using CoreLearning.DBLibrary.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreLearning.Infrastructure.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureBusiness(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                     AddJwtBearer(options =>
                                  {
                                      options.RequireHttpsMetadata = false;
                                      options.TokenValidationParameters = new TokenValidationParameters
                                                                          {
                                                                              ValidateIssuer = true,
                                                                              ValidIssuer = configuration["TokenAuthOptions:Issuer"],
                                                                              ValidateAudience = true,
                                                                              ValidAudience = configuration["TokenAuthOptions:Audience"],
                                                                              ValidateLifetime = true,
                                                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["TokenAuthOptions:Key"])),
                                                                              ValidateIssuerSigningKey = true
                                                                          };
                                  });

            services.AddTransient<ITokenService, TokenService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
using System.Collections.Generic;
using System.Text;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business;
using CoreLearning.Infrastructure.Business.Mediators;
using CoreLearning.Infrastructure.Business.Mediators.Handlers;
using CoreLearning.Infrastructure.Data;
using CoreLearning.Infrastructure.Data.Repositories;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CoreLearning.MessengerPrototype
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MessengerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                     AddJwtBearer(options =>
                                  {
                                      options.RequireHttpsMetadata = false;
                                      options.TokenValidationParameters = new TokenValidationParameters
                                                                          {
                                                                              ValidateIssuer = true,
                                                                              ValidIssuer = Configuration[ "TokenAuthOptions:Issuer" ],
                                                                              ValidateAudience = true,
                                                                              ValidAudience = Configuration[ "TokenAuthOptions:Audience" ],
                                                                              ValidateLifetime = true,
                                                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[ "TokenAuthOptions:Key" ])),
                                                                              ValidateIssuerSigningKey = true
                                                                          };
                                  });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICorrespondenceRepository, CorrespondenceRepository>();
            services.AddTransient<IFriendshipRepository, FriendshipRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAccountControllerHelper, AccountControllerHelper>();
            services.AddTransient<ISearchControllerHelper, SearchControllerHelper>();
            services.AddTransient<ICorrespondenceControllerHelper, CorrespondenceControllerHelper>();
            services.AddTransient<IUserSettingsControllerHelper, UserSettingsControllerHelper>();
            services.AddTransient<IFriendshipControllerHelper, FriendshipControllerHelper>();
            services.AddControllers();
            services.AddSwaggerGen(options =>
                                   {
                                       options.SwaggerDoc("v1", new OpenApiInfo {Title = "CoreLearning API", Description = "for showing swagger", Version = "v1"});
                                       options.AddSecurityDefinition("Bearer",
                                                                     new OpenApiSecurityScheme
                                                                     {
                                                                         Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                                                                         Name = "Authorization",
                                                                         In = ParameterLocation.Header,
                                                                         Type = SecuritySchemeType.ApiKey,
                                                                         Scheme = "Bearer"
                                                                     });

                                       options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                      {
                                                                          {
                                                                              new OpenApiSecurityScheme
                                                                              {
                                                                                  Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"},
                                                                                  Scheme = "oauth2",
                                                                                  Name = "Bearer",
                                                                                  In = ParameterLocation.Header
                                                                              },
                                                                              new List<string>()
                                                                          }
                                                                      });
                                   });

            services.AddMediatR(typeof( MediatorService ));
            services.AddValidatorsFromAssembly(typeof( SearchUsersHandler ).Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger(options => { options.SerializeAsV2 = true; });
            app.UseSwaggerUI(options =>
                             {
                                 options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                                 options.RoutePrefix = string.Empty;
                             });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
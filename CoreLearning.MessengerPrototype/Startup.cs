using System.Text;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business;
using CoreLearning.Infrastructure.Data;
using CoreLearning.Infrastructure.Data.Repositories;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
                                                                              ValidIssuer = "MyAuthServer", //Configuration[ "TokenAuthOptions:Issuer" ],
                                                                              ValidateAudience = true,
                                                                              ValidAudience = "MyAuthClient", //Configuration[ "TokenAuthOptions:Audience" ],
                                                                              ValidateLifetime = true,
                                                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("somthingstring!123+-" /*Configuration[ "TokenAuthOptions:Key" ]*/)),
                                                                              ValidateIssuerSigningKey = true
                                                                          };
                                  });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICorrespondenceRepository, CorrespondenceRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAccountControllerHelper, AccountControllerHelper>();
            services.AddTransient<ISearchControllerHelper, SearchControllerHelper>();
            services.AddTransient<ICorrespondenceControllerHelper, CorrespondenceControllerHelper>();
            services.AddTransient<IUserSettingsControllerHelper, UserSettingsControllerHelper>();
            services.AddTransient<IFriendshipControllerHelper, FriendshipControllerHelper>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
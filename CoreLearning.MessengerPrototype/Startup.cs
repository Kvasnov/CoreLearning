using System.Collections.Generic;
using CoreLearning.Infrastructure.Business;
using CoreLearning.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddInfrastructureData(Configuration);
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

            services.AddInfrastructureBusiness(Configuration);
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
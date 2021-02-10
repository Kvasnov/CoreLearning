using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CoreLearning.MessengerPrototype
{
    public class Startup
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).
                     AddJwtBearer( options =>
                                   {
                                       options.RequireHttpsMetadata = false;
                                       options.TokenValidationParameters = new TokenValidationParameters
                                                                           {
                                                                               ValidateIssuer = true,
                                                                               ValidIssuer = TokenAuthOptions.ISSUER,
                                                                               ValidateAudience = true,
                                                                               ValidAudience = TokenAuthOptions.AUDIENCE,
                                                                               ValidateLifetime = true,
                                                                               IssuerSigningKey = TokenAuthOptions.GetSymmetricSecurityKey(),
                                                                               ValidateIssuerSigningKey = true
                                                                           };
                                   } );

            services.AddControllers();
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints( endpoints => { endpoints.MapGet( "/", async context => { await context.Response.WriteAsync( "Hello World!" ); } ); } );
        }
    }
}
using CoreLearning.Infrastructure.Data;
using CoreLearning.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreLearning.MessengerPrototype
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddDbContext< UserContext >( options => options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) );
            //services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).
            //         AddJwtBearer( options =>
            //                       {
            //                           options.RequireHttpsMetadata = false;
            //                           options.TokenValidationParameters = new TokenValidationParameters
            //                                                               {
            //                                                                   ValidateIssuer = true,
            //                                                                   ValidIssuer = TokenAuthOptions.ISSUER,
            //                                                                   ValidateAudience = true,
            //                                                                   ValidAudience = TokenAuthOptions.AUDIENCE,
            //                                                                   ValidateLifetime = true,
            //                                                                   IssuerSigningKey = TokenAuthOptions.GetSymmetricSecurityKey(),
            //                                                                   ValidateIssuerSigningKey = true
            //                                                               };
            //                       } );
            services.AddTransient< IUserRepository, UserRepository >();
            services.AddControllers();
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints( endpoints => { endpoints.MapControllers(); } );
        }
    }
}
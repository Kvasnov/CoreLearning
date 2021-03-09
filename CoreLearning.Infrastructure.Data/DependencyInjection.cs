using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLearning.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MessengerContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICorrespondenceRepository, CorrespondenceRepository>();
            services.AddTransient<IFriendshipRepository, FriendshipRepository>();

            return services;
        }
    }
}
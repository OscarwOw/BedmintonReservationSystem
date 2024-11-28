using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess;
using Application.Interfaces;
using Application.BusinessLogic;

namespace Presentation
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabaseAccess>(new DatabaseAccess(connectionString));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILoginService, LoginService>();

            return services;
        }
        
    }
}

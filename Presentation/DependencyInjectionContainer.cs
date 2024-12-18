﻿using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess;
using Application.Interfaces;
using Application.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, string connectionString)
        {
            //singletons
            services.AddSingleton<IDatabaseAccess>(new DatabaseAccess(connectionString));
            services.AddSingleton<ILoginCacheService>(new LoginCacheService());
            services.AddSingleton<ICustomLogger>(new CustomLogger());
            

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<ICourtRepository, CourtRepository>();


            //services
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IReservationService, ReservationService>();


            return services;
        }
        
    }
}

using System;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Identity;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlServer(dbConnectionString));
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<UserStore<ApplicationUser, IdentityRole<Guid>, ApplicationDbContext, Guid>>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<IPostsRepository, PostsRepository>();

            services.AddTransient<ILikesRepository, PostsRepository>();
           
            return services;
        }
    }
}
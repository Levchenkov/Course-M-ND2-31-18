using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Repositories;

namespace Twitter.Infrastructure
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return service;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection service)
        {
            service.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return service;
        }
    }
}

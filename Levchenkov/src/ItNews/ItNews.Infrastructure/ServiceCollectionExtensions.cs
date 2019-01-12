using ItNews.Common.Contracts.Configurations;
using ItNews.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItNews.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings.ApplicationDbContext));

            return services;
        }
    }
}

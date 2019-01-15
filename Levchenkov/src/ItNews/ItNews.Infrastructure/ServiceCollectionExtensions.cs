using ItNews.Common.Contracts.Configurations;
using ItNews.Data.Contracts;
using ItNews.Data.EntityFramework;
using ItNews.Domain.Contracts;
using ItNews.Domain.Services;
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

        public static IServiceCollection AddDataDependencies(this IServiceCollection services)
        {
            services
                .AddScoped<IDbScopeFactory>(serviceProvider =>
                {
                    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    var factory = new DbScopeFactory(() => dbContext);
                    return factory;
                });

            return services;
        }

        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {
            services
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<IProfileService, ProfileService>()
                .AddScoped<IRatingService, RatingService>()
                .AddScoped<ITagService, TagService>();

            return services;
        }
    }
}

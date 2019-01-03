using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Context;
using AutoMapper;
using Twitter.Domain.Contracts.Services;
using Twitter.Data.Contracts.Repositories;
using Twitter.Repositories;
using Microsoft.AspNetCore.Mvc;
using Twitter.Domain.Contracts.InfoModels;
using Twitter.Web.Hubs;

namespace Twitter.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper();


            services.ConfigureApplicationCookie(options => options.LoginPath = "/User/LogIn");
            services.AddSignalR();  

            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IUserService<IUserInfo>, UserService>();
            services.AddTransient<IUserRepository<ApplicationUser>, UserRepository>();
            services.AddTransient<IUserInfo, UserInfo>();
            services.AddTransient<ITwitInfo, TwitInfo>();
            services.AddTransient<ITwitService, TwitService>();
            services.AddTransient<ITwitRepository, TwitRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseSignalR(routes =>  
            {
                routes.MapHub<ReportsPublisher>("hubs");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=temp}/{action=Index}/{id?}");
            });
        }
    }
}

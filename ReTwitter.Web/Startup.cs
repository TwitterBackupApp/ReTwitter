using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReTwitter.Data;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.TwitterApiService;
using ReTwitter.Services.External;
using ReTwitter.Services.External.Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Statistics;
using ReTwitter.Web.Extensions;

namespace ReTwitter.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterData(services);
            this.RegisterAuthentication(services);
            this.RegisterServices(services);
            this.RegisterInfrastructure(services);
        }

        private void RegisterData(IServiceCollection services)
        {
            services.AddDbContext<ReTwitterDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private void RegisterAuthentication(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ReTwitterDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["ConsumerSecret"];
            });

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }
            else
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 4;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                });
            }
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITwitterCredentialsProvider, TwitterCredentialsProvider>();
            services.AddTransient<ITwitterApiCaller, TwitterApiCaller>();
            services.AddTransient<IJsonDeserializer, JsonDeserializer>();
            services.AddTransient<ICascadeDeleteService, CascadeDeleteService>();
            services.AddTransient<IFolloweeService, FolloweeService>();
            services.AddTransient<IFolloweeStatisticsService, FolloweeStatisticsService>();
            services.AddTransient<ITweetStatisticsService, TweetStatisticsService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDateTimeParser, DateTimeParser>();
            services.AddTransient<IAdminUserService, AdminUserService>();
            services.AddTransient<IUserFolloweeService, UserFolloweeService>();
            services.AddTransient<IUserTweetService, UserTweetService>();
            services.AddTransient<ITweetTagService, TweetTagService>();
            services.AddTransient<ITwitterApiCallService, TwitterApiCallService>();
            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<ITagService, TagService>();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Hourly", new CacheProfile()
                {
                    Duration = 60 * 60
                });
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
            services.AddAutoMapper();
            
            services.AddScoped<IMappingProvider, MappingProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
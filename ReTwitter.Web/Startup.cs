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
using ReTwitter.Services.Data.Statistics;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterData(services);
            this.RegisterAuthentication(services);
            this.RegisterServices(services);
            this.RegisterInfrastructure(services);
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            services.AddScoped<IMappingProvider, MappingProvider>();
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
            services.AddTransient<IUserStatisticsService, UserStatisticsService>();
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
                    options.Password.RequiredLength = 8;
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                DbInitializer.Seed(serviceProvider,
                                  (ITwitterApiCaller)serviceProvider.GetService(typeof(ITwitterApiCaller)),
                                  (IMappingProvider)serviceProvider.GetService(typeof(IMappingProvider)));
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

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Inveon.Admin.Filters;
using Inveon.Core.Interfaces.CacheManagement;
using Inveon.Core.Interfaces.DbConfigs;
using Inveon.Core.Interfaces.Helpers;
using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Interfaces.Services;
using Inveon.Data.CacheManagement;
using Inveon.Data.DbConfig;
using Inveon.Data.Repositories;
using Inveon.Service.Helpers;
using Inveon.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace Inveon.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.Configure<MongoConfiguration>(
               Configuration.GetSection(nameof(MongoConfiguration)));

            services.AddSingleton<IMongoConfiguration>(sp =>
                sp.GetRequiredService<IOptions<MongoConfiguration>>().Value);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login/";
                    options.AccessDeniedPath = new PathString("/Account/Denied");
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });
            

            #region AutoFac

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<RedisConfiguration>().As<IRedisConfiguration>().SingleInstance();
            builder.RegisterType<RedisManagement>().As<IRedisManagement>().SingleInstance();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();

            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<ProductServiceHelper>().As<IProductServiceHelper>().SingleInstance();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterType<AccountFilter>();
            builder.RegisterType<NotFoundFilter>();

            #endregion

            var appContainer = builder.Build();
            return new AutofacServiceProvider(appContainer);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}

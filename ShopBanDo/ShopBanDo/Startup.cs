using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopBanDo.Extension;
using ShopBanDo.Interface;
using ShopBanDo.MiddleWare;
using ShopBanDo.Models;
using ShopBanDo.Repositories;
using ShopBanDo.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ShopBanDo
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
            //connectionString trong appsettings.json
            var stringConnectdb = Configuration.GetConnectionString("dbShopBanDo");
            services.AddDbContext<dbshopContext>(options => options.UseSqlServer(stringConnectdb));
            //chinh phong chu tieng viet thanh unicode
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
            //serices NotifY
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //vi tri cua services notify
            services.AddNotyf(config => { 
                config.DurationInSeconds = 5; 
                config.IsDismissable = true; 
                config.Position = NotyfPosition.BottomRight; });
            //add services session
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromDays(1));
            //add authen cho cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(p =>
                {
                    p.Cookie.Name = "UserLoginCookie";
                    p.ExpireTimeSpan = TimeSpan.FromDays(1);
                    p.LoginPath = "/Admin/AdminAccounts/Login";
                    //p.LoginPath = "/dang-nhap.html";
                    p.LogoutPath = "/Home/Index";
                    p.AccessDeniedPath = "/Admin/AccessDenied";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", options => options.RequireAssertion(context=>context.User.HasClaim(claim=>claim.Value=="1"||claim.Value=="4")));
                options.AddPolicy("Staff", options => options.RequireAssertion(context=>context.User.HasClaim(claim=>claim.Value=="1"||claim.Value=="2"||claim.Value=="4")));
            });
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IOrderRepository, OrderRespository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //su dung wwwroot enable static file to served to client
            app.UseStaticFiles();

            app.UseRouting();
            //su dung authentication
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseSession();

            app.RegisterMiddleware();

            app.UseEndpoints(endpoints =>
            {
                //Route Admin
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

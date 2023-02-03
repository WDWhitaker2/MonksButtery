using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonksInn.Domain.Interfaces;
using MonksInn.SmtpEmailService;
using MonksInn.SqlDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.Cookie.Name = "MonksWebAppAuthCookie"; // Name of cookie   
                config.LoginPath = "/Account/Login"; // Path for the redirect to user login page  
                config.AccessDeniedPath = "/Account/AccessDenied";// TODO create this view
            });

            services.AddDbContext<SqlDataContext>(item => item.UseSqlServer(Configuration.GetConnectionString("MonksDB")));


            services.AddScoped<IEmailTemplateService, RazorEmailTemplateService.RazorEmailTemplateService>();
            services.AddScoped<IEmailService, SmtpEmailService.SmtpEmailService>();
            services.AddScoped<IDatabaseService, SqlDataService.SqlDataService>();

            services.AddScoped<IUnitOfWork, WebUnitOfWork>();



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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
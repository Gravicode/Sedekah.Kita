using System.Net.Http;
using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SedekahKita.Web.Data;
using SedekahKita.Tools;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace SedekahKita.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppConstants.SQLConn = Configuration["ConnectionStrings:SqlConn"];
            AppConstants.BlobConn = Configuration["ConnectionStrings:BlobConn"];
            AppConstants.GMapApiKey = Configuration["GmapKey"];
            services.AddBlazoredSessionStorage();
            services.AddBlazoredLocalStorage();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredToast();
            
            // ******
            // BLAZOR COOKIE Auth Code (begin)
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            // BLAZOR COOKIE Auth Code (end)
            // ******
            // ******
            // BLAZOR COOKIE Auth Code (begin)
            // From: https://github.com/aspnet/Blazor/issues/1554
            // HttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            // BLAZOR COOKIE Auth Code (end)
            // ******
            MailService.MailUser = Configuration["MailSettings:MailUser"];
            MailService.MailPassword = Configuration["MailSettings:MailPassword"];
            MailService.MailServer = Configuration["MailSettings:MailServer"];
            MailService.MailPort = int.Parse(Configuration["MailSettings:MailPort"]);
            SmsService.UserKey = Configuration["SmsSettings:ZenzivaUserKey"];
            SmsService.PassKey = Configuration["SmsSettings:ZenzivaPassKey"];
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("103.189.234.251"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // ******
            // BLAZOR COOKIE Auth Code (begin)
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            // BLAZOR COOKIE Auth Code (end)
            // ******
            app.UseEndpoints(endpoints =>
            { 
                // BLAZOR COOKIE Auth Code (begin)
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                // BLAZOR COOKIE Auth Code (end)
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            var db = new SedekahDB();
            db.Database.EnsureCreated();
        }
    }
}

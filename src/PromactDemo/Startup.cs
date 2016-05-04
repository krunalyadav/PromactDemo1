using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromactDemo.Models;

namespace PromactDemo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddSqlServer().AddDbContext<RestaurantDbContext>();
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = false;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
            }).AddEntityFrameworkStores<RestaurantDbContext>().AddDefaultTokenProviders();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Default", template: "{controller=Restaurant}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

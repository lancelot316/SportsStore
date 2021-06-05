using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.Models.Domain.Repository;
using SportsStore.Web.Models.Domain;
using SportsStore.Web.Models;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<EFDbContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:EFDbContext"]);
            });

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<IProductRepository, StoreRepository>();
            services.AddScoped<IOrderRepository, StoreRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(null,
                "",
                new
                {
                    controller = "Product",
                    action = "List",
                    category = (string)null,
                    page = 1
                });

                endpoints.MapControllerRoute(
                    "catpage",
                    "{category}/Page{page:int}",
                    new { Controller = "Product", action = "List" });

                endpoints.MapControllerRoute(
                    "page", 
                    "Page{page:int}",
                    new { Controller = "Product", action = "List", page = 1 });
                
                endpoints.MapControllerRoute(
                    "category", 
                    "{category}",
                    new { Controller = "Product", action = "List", page = 1 });
                
                endpoints.MapControllerRoute(
                    "pagination",
                    "Page{page}",
                    new { Controller = "Product", action = "List", page = 1 });
                
                
                endpoints.MapControllerRoute(null, "{controller}/{action}");

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

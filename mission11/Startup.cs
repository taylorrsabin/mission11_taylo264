using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mission11.Models;

namespace mission11
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();

            //Enable Razor Pages
            services.AddRazorPages();

            //Add sessions
            services.AddDistributedMemoryCache();
            services.AddSession();

            //Allow session to get basket, or create a new one if one doesn't already exist
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //Enable sessions and routing
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // First check if category is selected and page number
                endpoints.MapControllerRoute(
                    "typepage",
                    "{bookType}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //Next check for just page number
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index" }
                    );

                //Next Check for just category
                endpoints.MapControllerRoute("type", "{Category}", new { Controller = "Home", action = "Index", pageNum = 1 });

                //Default
                endpoints.MapDefaultControllerRoute();

                //Enable razor pages
                endpoints.MapRazorPages();
            });
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net_simple_onlineshop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace asp.net_simple_onlineshop
{
    public class Startup
    {
        // instance of IConfiguration
        public IConfiguration Configuration { get; }


        // Constructor Injection
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Connection service with Entity Framework
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // register a service with its interface into the services collection

            // services with real database implementation
            services.AddScoped<IPieRepository, PieRepository>(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            // invoke for the user the GetCart method on the ShoppingCart class. I pass in IService Provider, that is the services collection that I need in my GetCard methods.
            // create scope shopping cart using the getcart method 
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            // remember in the shoppingcart.getcart we used the IHttpContextAccessor that would then give us access to the session.
            services.AddHttpContextAccessor();

            // services with mock implementation
            // services.AddScoped<IPieRepository, MockPieRepository>();
            // services.AddScoped<ICategoryRepository, MockCategoryRepository>();

            services.AddSession(); // to register a service


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Midleware
            // Note: Order of the middleware definitely is important
            app.UseHttpsRedirection(); // Redirects http request to HTTPS 
            app.UseStaticFiles(); // will search in a directory called wwwroot for static files.
            app.UseSession(); // make use this comes first before UseRouting
            // End middleware


            //Enable mvc to respond to incoming request needs to map an incoming request with the correct code what will execute.
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

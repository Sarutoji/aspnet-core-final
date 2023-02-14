using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;
using ToDoWebApp.Models.Data;
using ToDoWebApp.Models.Repository;

namespace ToDoWebApp
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

            services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DevelopmentConnection"]);
            });

            services.AddScoped<IToDoRepository, EFToDoRepository>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("importantpage",
                    "{completed}/{important}/Page{page:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("completedpage",
                    "{completed}/Page{page:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("taskpage", "Task/{listId}/{completed}/Page{page:int}",
                    new { Controller = "Task", action = "Index" });

                //endpoints.MapControllerRoute("taskpage", "Task/{listId}/Page{page:int}",
                //    new { Controller = "Task", action = "Index" });

                endpoints.MapControllerRoute("page", "Page{page:int}",
                    new { Controller = "Home", action = "Index", listPage = 1 });
               
                endpoints.MapControllerRoute("important", "{comleted}",
                    new {Controller = "Home", action = "Index", listPage = 1 });
                
                endpoints.MapControllerRoute("pagination", "Lists/Page{page}",
                    new { Controller = "Home", action = "Index", listPage = 1 });
               
                endpoints.MapControllerRoute("createList", "Create/{createList}",
                    new { Controller = "Home", action = "CreateList" });
                
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}

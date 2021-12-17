using DateTimeProblems.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace OldStyle
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

            services.AddDbContext<PeopleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PeopleContext context)
        {
            InitializeDb(context);

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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=People}/{action=Index}/{id?}");
            });
        }

        private void InitializeDb(PeopleContext context)
        {
            context.Database.EnsureDeleted();   
            context.Database.EnsureCreated();

            context.People.Add(new Person { Name = "Fred", DateOfBirth = new DateTime(1990,3,1)});
            context.People.Add(new Person { Name = "Wilma", DateOfBirth = new DateTime(1995,4,2)});
            context.People.Add(new Person { Name = "Betty", DateOfBirth = new DateTime(1997,9,10)});
            context.People.Add(new Person { Name = "Barney", DateOfBirth = new DateTime(1991,6,6)});

            context.SaveChanges();
        }
    }
}

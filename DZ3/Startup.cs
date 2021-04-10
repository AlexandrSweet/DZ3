using BusinessLayer.DataProviderProfilerService;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace DZ3
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
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(@"Server = DESKTOP-D5NKT8U;Database = EmployeesInfo;Integrated Security = True;",
                    b => b.MigrationsAssembly("DataAccessLayer"));
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IDataProviderProfilerService, DataProviderProfilerService>();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            SeedDefault(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
        private void SeedDefault(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Employees.FirstOrDefault() == null)
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        dbContext.Employees.Add(new Employees { FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString() });
                        
                    }
                    dbContext.SaveChanges();

                    var employeess = dbContext.Employees.ToList();
                    for (int j = 0; j < 2; j++)
                    {
                        for (int i = 0; i < employeess.Count; i++)
                        {
                            dbContext.HiringHistories.Add(new HiringHistories { CompanyName = Guid.NewGuid().ToString(), EmployeesId = employeess[i].Id });
                            
                        }
                        dbContext.SaveChanges();
                    }

                    var hiringHistoris = dbContext.HiringHistories.ToList();
                    for (int j = 0; j < 2; j++)
                    {
                        for (int i = 0; i < hiringHistoris.Count; i++)
                        {
                            dbContext.Achievements.Add(new Achievements { AchievementData = Guid.NewGuid().ToString(), HiringHistoriesId = hiringHistoris[i].Id });
                            
                        }
                        dbContext.SaveChanges();
                    }
                }

            }

        }
    }
}

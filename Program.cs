using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.data;
using TestMVC.Models;

namespace TestMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<TestMvcDbContextDbContext>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                // Create the Db if it doesn't exist and applies any pending migration.
                dbContext.Database.EnsureCreated();
                // Seed the Db
                DbSeeder.Seed(dbContext, roleManager, userManager);
            }
            host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
               .Build();
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}

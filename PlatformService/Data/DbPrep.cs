using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public class DbPrep
    {
        public static void PrepareDB(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                Seed(scope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void Seed(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        } 
    }
}

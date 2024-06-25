using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObjectDetection.Domain.Entities;
using ObjectDetection.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Entity;

namespace ObjectDetection.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddDbContextPool<ObjectDetectionDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ObjectDetectionConnection")).UseLowerCaseNamingConvention());
            //services.AddDbContextPool<ObjectDetectionDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("ObjectDetectionConnection")).UseLowerCaseNamingConvention());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ConnStr>();

            services.AddDbContextPool<ObjectDetectionDbContext>((serviceProvider, options) =>
            {
                var connStr = serviceProvider.GetRequiredService<ConnStr>().Get();
                options.UseNpgsql(connStr).UseLowerCaseNamingConvention();
            });


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._";
            }
            ).AddEntityFrameworkStores<ObjectDetectionDbContext>();

       
        }

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<ObjectDetectionDbContext>();
            dataContext.Database.Migrate();
        }
    }
}

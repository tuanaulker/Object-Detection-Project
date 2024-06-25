using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ObjectDetection.Application.Features.User.Mapper;
using ObjectDetection.CommonModels.Repositories;
using ObjectDetection.Domain;
using ObjectDetection.Domain.Entities;
using ObjectDetection.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IUnitOfWork<Log>, UnitOfWork<Log>>();
            services.AddScoped<GenericService<Log>>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

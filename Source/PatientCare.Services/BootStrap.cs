using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PatientCare.Services.AutoMapperProfiles;
using PatientCare.Services.Interfaces;
using System;

namespace PatientCare.Services
{
    public class BootStrap
    {
        public static IServiceCollection SetupDependencies(IServiceCollection services)
        {
            var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
                                        cfg.AddProfile<AutoMapperProfile>());

            autoMapperConfig.AssertConfigurationIsValid();
            services.AddSingleton<AutoMapper.IMapper>(autoMapperConfig.CreateMapper());

            Repository.BootStrap.SetupDependencies(services);

            services
                .AddTransient<IPatientService, PatientService>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using PatientCare.Repository.Interfaces;
using System;

namespace PatientCare.Repository
{
    public class BootStrap
    {
        public static IServiceCollection SetupDependencies(IServiceCollection services)
        {
            services
                .AddTransient<IImmunisationRepository, ImmunisationRepository>()
                .AddTransient<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}

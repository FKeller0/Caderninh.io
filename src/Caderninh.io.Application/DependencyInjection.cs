using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Caderninh.io.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));                
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}
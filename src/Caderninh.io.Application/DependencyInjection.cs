using Microsoft.Extensions.DependencyInjection;

namespace Caderninh.io.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            return services;
        }
    }
}
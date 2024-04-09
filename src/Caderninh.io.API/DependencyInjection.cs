using Caderninh.io.API.Services;
using Caderninh.io.Application.Common.Interfaces;

namespace Caderninh.io.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddProblemDetails();
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            return services;
        }
    }
}
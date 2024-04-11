using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Common.Interfaces;
using Caderninh.io.Infrastructure.Authentication.JwtGenerator;
using Caderninh.io.Infrastructure.Authentication.PasswordHasher;
using Caderninh.io.Infrastructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using Caderninh.io.Infrastructure.Common.Persistence;
using Caderninh.io.Infrastructure.NoteCategories.Persistence;
using Caderninh.io.Infrastructure.Notes.Persistence;

namespace Caderninh.io.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AddAuthentication(configuration)
                .AddPersistence();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services) 
        {
            services.AddDbContext<CaderninhoDbContext>(options => options.UseSqlite("Data Source = Caderninhio.db"));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<INoteCategoriesRepository, NoteCategoriesRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CaderninhoDbContext>());

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration) 
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.Section, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });

            return services;
        }
    }
}
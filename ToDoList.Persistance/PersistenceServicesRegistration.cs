using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Contracts.Persistence;
using ToDoList.Application.Contracts.Persistence.Auth;
using ToDoList.Application.Contracts.Persistence.Common;
using ToDoList.Domain;
using ToDoList.Persistance.Repositories;
using ToDoList.Persistance.Repositories.Auth;
using ToDoList.Persistance.Repositories.Common;

namespace ToDoList.Persistance
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ToDoListDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RazorAppConnectionString"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ToDoListDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ILoginHelperSessionRepository, LoginHelperSessionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;

        }
    }
}

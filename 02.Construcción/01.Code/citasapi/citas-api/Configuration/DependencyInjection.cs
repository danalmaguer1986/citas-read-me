using Infraestructura.DataAccess.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Negocio.Core;
using Negocio.DataAccess;
using Negocio.Shared;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace citas_api.Configuration
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {

            var business = Assembly.Load("Negocio");
            var infraStructure = Assembly.Load("Infraestructura");
            var current = Assembly.GetExecutingAssembly();

            services.AddScoped<UnitOfWork>();


            services.RegisterAssemblyPublicNonGenericClasses(business, infraStructure)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(business, infraStructure)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(current)
                .Where(c => c.Name.EndsWith("MessageHandler"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddSingleton<IDateTimeService, DateTimeService>();

            services.AddScoped<ILoggedUser, LoggedUser>();

            services.AddScoped<IRequestTypeGetter, RequestTypeGetter>();

            services.AddScoped<Dispatcher>();

            services.AddHandlers();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }
    }
}

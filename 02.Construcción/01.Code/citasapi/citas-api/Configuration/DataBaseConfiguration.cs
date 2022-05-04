using Infraestructura.DataAccess.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace citas_api.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataBaseConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddDataBase(this IServiceCollection services, IConfiguration config)
        {
            var sessionFactory = new SessionFactory(config.GetConnectionString("Main"), true);
            SessionFactory.Instance = sessionFactory;
            services.AddSingleton(sessionFactory);
        }
    }
}

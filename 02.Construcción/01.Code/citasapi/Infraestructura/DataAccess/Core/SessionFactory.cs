using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infraestructura.DataAccess.Core
{
    public class SessionFactory
    {
        private static readonly List<string> AssignedEntities = new List<string>
        {
            "Usuarios","Doctores"
        };

        public static SessionFactory Instance { get; set; }

        private readonly ISessionFactory _factory;

        public bool Connected { get; private set; }

        public string ConnectionError { get; private set; }

        protected SessionFactory()
        {

        }

        public SessionFactory(string connectionString, bool throwExceptionIfFail)
        {
            try
            {
                _factory = BuildSessionFactory(connectionString);
                this.Connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.InnerException?.Message);

                if (throwExceptionIfFail)
                {
                    throw;
                }

                this.ConnectionError = e.Message;
                this.Connected = false;

            }
        }

        public ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(string connectionString)
        {



            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                        .AddFromAssembly(Assembly.GetExecutingAssembly())
                        .Conventions.Add(
                            ForeignKey.EndsWith("Id"),
                            ConventionBuilder.Property
                                .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))
                        .Conventions.Add<TableNameConvention>()
                        .Conventions.Add<CustomConvention>()
                )
                .Cache(c => c.ProviderClass(typeof(NHibernate.Caches.CoreMemoryCache.CoreMemoryCacheProvider).AssemblyQualifiedName)
                    .UseSecondLevelCache()
                    .UseQueryCache()
                )
                .ExposeConfiguration(x =>
                {
                    x.Cache(e => e.DefaultExpiration = 1800);
                });

            return configuration.BuildSessionFactory();

        }

        public class TableNameConvention : IClassConvention
        {
            public void Apply(IClassInstance instance)
            {
                instance.Table(instance.EntityType.Name);
            }
        }

        public class CustomConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {

                if (AssignedEntities.Contains(instance.EntityType.Name))
                {
                    instance.GeneratedBy.Assigned();
                }
                else
                {
                    instance.GeneratedBy.Increment();
                }
            }
        }
    }
}

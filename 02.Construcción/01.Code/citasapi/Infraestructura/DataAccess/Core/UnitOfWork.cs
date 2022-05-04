
using Negocio.Core;
using Negocio.Shared;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace Infraestructura.DataAccess.Core
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly ILoggedUser _auditUser;
        private readonly IRequestTypeGetter _requestTypeGetter;
        private ISession _session;
        private ITransaction _transaction;
        private bool _isAlive = true;

        public UnitOfWork(SessionFactory sessionFactory)
        {
            this.StartSession(sessionFactory);
        }

        public UnitOfWork(SessionFactory sessionFactory, ILoggedUser user, IRequestTypeGetter requestTypeGetter)
        {
            this._requestTypeGetter = requestTypeGetter;
            this._auditUser = user;
            this.StartSession(sessionFactory);

        }

        public T Get<T>(int id) where T : class
        {
            return _session.Get<T>(id);
        }

        public void Save<T>(T entity) where T : Entity
        {
            this.SetAudits(entity);

            try
            {
                _session.SaveOrUpdate(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Merge<T>(T entity) where T : Entity
        {
            this.SetAudits(entity);
            _session.Merge(entity);
        }

        public void Delete<T>(T entity)
        {
            _session.Delete(entity);
            _session.Flush();
        }

        public void UpdateAll<T>(List<T> source, Expression<Func<T, bool>> where = null) where T : Entity
        {
            var current = where == null ? _session.Query<T>().ToList() : _session.Query<T>().Where(@where).ToList();

            var doesNotExists = current.Where(e => source.All(c => c != e)).ToList();

            foreach (var notExist in doesNotExists)
            {
                _session.Delete(notExist);
            }


            foreach (var newItem in source)
            {
                this.SetAudits(newItem);

                var actual = current.FirstOrDefault(e => e.Id == newItem.Id);

                if (actual != null)
                {
                    _session.Merge(newItem);
                }
                else
                {
                    _session.Save(newItem);
                }
            }
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        private bool IsGetOperation()
        {
            return this._requestTypeGetter != null && this._requestTypeGetter.GetRequestType().Equals("Get", StringComparison.InvariantCultureIgnoreCase);
        }

        private void StartSession(SessionFactory sessionFactory)
        {

            if (!sessionFactory.Connected)
            {
                throw new Exception("No existe conexión con la base de datos");
            }

            _session = sessionFactory.OpenSession();


            _session.SetBatchSize(100);

            if (this.IsGetOperation())
            {
                _session.DefaultReadOnly = true;
            }
            else
            {
                _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void Dispose()
        {
            if (!_isAlive)
                return;

            _isAlive = false;

            _transaction?.Dispose();
            _session.Close();
            _session.Dispose();
        }

        public void Commit()
        {
            if (!_isAlive || _transaction == null)
                return;

            _transaction.Commit();
        }

        private void SetAudits<T>(T entity) where T : Entity
        {
            if (!(entity is IAuditable auditable)) return;

            // esto se debe inyectar en las pruebas
            auditable.AuditUser = this._auditUser?.UserName ?? "";
            auditable.AuditDate = DateTime.UtcNow;
        }


    }
}

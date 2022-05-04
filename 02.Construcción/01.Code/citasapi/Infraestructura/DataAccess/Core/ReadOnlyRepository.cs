using CSharpFunctionalExtensions;
using Negocio.DataAccess;
using Negocio.Shared;
using System.Linq;
using Entity = Negocio.Core.Entity;
namespace Infraestructura.DataAccess.Core
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        private readonly UnitOfWork _context;

        public ReadOnlyRepository(UnitOfWork context)
        {
            _context = context;
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Query<T>();
        }

        public Result<T> Get<T>(int id) where T : Entity
        {
            var item = _context.Get<T>(id);
            return item ?? Result.Failure<T>(Errors.General.EntityNotFound(typeof(T).Name));
        }

        public T GetOrDefault<T>(int id) where T : class
        {
            return _context.Get<T>(id);
        }
    }
}

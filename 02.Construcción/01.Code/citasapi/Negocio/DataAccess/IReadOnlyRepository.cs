using System.Linq;
using CSharpFunctionalExtensions;
using Entity = Negocio.Core.Entity;

namespace Negocio.DataAccess
{
    public interface IReadOnlyRepository
    {
        IQueryable<T> Query<T>() where T : Entity;
        Result<T> Get<T>(int id) where T : Entity;
        T GetOrDefault<T>(int id) where T : class;
    }
}

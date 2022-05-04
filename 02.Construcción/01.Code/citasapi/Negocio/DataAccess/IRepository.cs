using Negocio.Core;
using System.Collections.Generic;
using System.Linq;
namespace Negocio.DataAccess
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Query();
        TEntity Get(int id);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void SaveAll(IEnumerable<TEntity> entities);

        void Commit();
    }
}

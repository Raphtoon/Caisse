using System.Linq.Expressions;

namespace Caisse.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity produit);
        bool Delete(int id);
        bool Update(TEntity produit);
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity? GetById(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoubleVPartners.Infrastructure.DataAccess.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(object id);
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> GetByProperty(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        void Clear();
    }
}

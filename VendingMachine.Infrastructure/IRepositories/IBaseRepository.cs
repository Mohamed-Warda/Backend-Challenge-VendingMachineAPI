using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IDbContextTransaction> BeginTransaction();
        Task CommitTranaction(IDbContextTransaction transaction);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllWithExpressonAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> DeleteRange(IEnumerable<TEntity> entities);
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> Delete(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

    }
}

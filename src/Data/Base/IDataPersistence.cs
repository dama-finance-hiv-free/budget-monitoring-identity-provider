using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityProvider.Data.Base
{
    public interface IDataPersistenceBase { }

    public interface IDataPersistence<TEntity> : IDisposable, IDataPersistenceBase where TEntity : class
    {
        TEntity Get(TEntity entity);
        TEntity Get(string entity);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetAsync(TEntity entity);
        Task<TEntity> GetAsync(string entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        TEntity[] GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity[] GetMany(Expression<Func<TEntity, bool>> where);
        Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, string>> orderBy);
        Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy);
        Task<RepositoryActionResult<TEntity>> AddAsync(TEntity multiFactorType);
        Task<RepositoryActionResult<IEnumerable<TEntity>>> AddManyAsync(IEnumerable<TEntity> entities);
        Task<RepositoryActionResult<TEntity>> EditAsync(TEntity entity);
        Task<RepositoryActionResult<TEntity>> DeleteAsync(TEntity entity);
        Task<RepositoryActionResult<TEntity>> DeleteManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetFirstOrDefaultAsync();
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> where);
    }
}

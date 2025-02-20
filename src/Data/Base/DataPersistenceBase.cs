using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data.Base
{
    public abstract class DataPersistenceBase<TEntity, TContext> : Disposable, IDataPersistence<TEntity>
            where TEntity : class, IIdentifiableEntity, new()
            where TContext : DbContext
    {
        protected TContext Context;
        protected IDbConnection Db;

        protected DbSet<TEntity> DbSet { get; }

        protected DataPersistenceBase(TContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity multiFactorType)
        {
            try
            {
                DbSet.Add(multiFactorType);
                var result = await SaveChangesAsync();
                return result != 0
                    ? new RepositoryActionResult<TEntity>(multiFactorType, RepositoryActionStatus.Created)
                    : new RepositoryActionResult<TEntity>(multiFactorType, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public virtual async Task<RepositoryActionResult<IEnumerable<TEntity>>> AddManyAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                DbSet.AddRange(enumerable);
                var result = await SaveChangesAsync();
                return result != 0
                    ? new RepositoryActionResult<IEnumerable<TEntity>>(enumerable, RepositoryActionStatus.Created)
                    : new RepositoryActionResult<IEnumerable<TEntity>>(enumerable,
                        RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<IEnumerable<TEntity>>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public virtual async Task<RepositoryActionResult<TEntity>> DeleteAsync(TEntity entity)
        {
            try
            {
                var existingEntity = await ItemToGetAsync(entity);
                if (existingEntity == null)
                    return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
                Context.Entry(existingEntity).State = EntityState.Deleted;
                var result = await SaveChangesAsync();
                return result > 0
                    ? new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Deleted)
                    : new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public virtual async Task<RepositoryActionResult<TEntity>> DeleteManyAsync(
            Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = DbSet.Where(where).AsEnumerable();
                foreach (var entity in entities)
                {
                    DbSet.Remove(entity);
                }

                await SaveChangesAsync();
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Deleted);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public virtual async Task<RepositoryActionResult<TEntity>> EditAsync(TEntity entity)
        {
            try
            {
                var existingEntity = await ItemToGetAsync(entity);
                if (existingEntity == null)
                {
                    return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
                }

                Context.Entry(existingEntity).State = EntityState.Detached;
                existingEntity = PropertyMapper.PropertyMapSelective(entity, existingEntity);
                DbSet.Attach(existingEntity);
                Context.Entry(existingEntity).State = EntityState.Modified;
                var result = await SaveChangesAsync();
                return result == 0
                    ? new RepositoryActionResult<TEntity>(existingEntity, RepositoryActionStatus.NothingModified, null)
                    : new RepositoryActionResult<TEntity>(existingEntity, RepositoryActionStatus.Updated);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public virtual TEntity Get(TEntity entity) => ItemToGet(entity);

        public virtual TEntity Get(string entity) => ItemToGet(entity);

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where) => GetMany(where).FirstOrDefault();

        public virtual TEntity[] GetAll() => ItemsToGet();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await ItemsToGetAsync();

        public virtual async Task<TEntity> GetAsync(TEntity entity) => await ItemToGetAsync(entity);

        public virtual async Task<TEntity> GetAsync(string entity) => await ItemToGetAsync(entity);

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.Where(where).FirstOrDefaultAsync();

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> where) =>
            DbSet.AsNoTracking().FirstOrDefault(where);

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.AsNoTracking().FirstOrDefaultAsync(where);

        public virtual async Task<TEntity> GetFirstOrDefaultAsync() =>
            await DbSet.AsNoTracking().FirstOrDefaultAsync();

        public virtual TEntity[] GetMany(Expression<Func<TEntity, bool>> where) =>
            DbSet.AsNoTracking().Where(where).ToArray();

        public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where) =>
                                                            await DbSet.AsNoTracking().Where(where).ToArrayAsync();

        public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, string>> orderBy) =>
            await DbSet.AsNoTracking().Where(where).OrderBy(orderBy).ToArrayAsync();

        public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy) =>
            await DbSet.AsNoTracking().Where(where).OrderBy(orderBy).ThenBy(thenBy).ToArrayAsync();

        protected override void DisposeCore()
        {
            Context?.Dispose();
            Context = null;
        }

        protected virtual TEntity[] ItemsToGet() => DbSet.AsNoTracking().ToArray();

        protected virtual async Task<TEntity[]> ItemsToGetAsync() => await DbSet.AsNoTracking().ToArrayAsync();

        protected virtual TEntity ItemToAdd(TEntity entity) => DbSet.Add(entity).Entity;

        protected virtual TEntity ItemToGet(TEntity entity) => entity;

        protected virtual TEntity ItemToGet(string entity) => new TEntity();

        protected virtual async Task<TEntity> ItemToGetAsync(TEntity entity) => await Task.FromResult(entity);

        protected virtual async Task<TEntity> ItemToGetAsync(string entity) => await Task.FromResult(new TEntity());

        protected int SaveChanges() => Context.SaveChanges();

        protected async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
    }

    public abstract class DataPersistenceBase<TEntity> : DataPersistenceBase<TEntity, ApplicationDbContext>
        where TEntity : class, IIdentifiableEntity, new()
    {
        protected IDatabaseFactory DatabaseFactory;

        protected DataPersistenceBase(IDatabaseFactory databaseFactory) : base(databaseFactory.GetContext())
        {
            DatabaseFactory = databaseFactory;
            Db = databaseFactory.GetConnection();
        }

        protected string ConnectionString => DatabaseFactory.GetContext().Database.GetDbConnection().ConnectionString;

        protected override void DisposeCore()
        {
            DatabaseFactory?.Dispose();
            base.DisposeCore();
        }
    }
}

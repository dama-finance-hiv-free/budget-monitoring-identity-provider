using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Core;
using IdentityProvider.Data.Base;

namespace IdentityProvider.Data.Repository.MultiFactorType
{
    public class MultiFactorTypePersistence : DataPersistenceBase<Entity.MultiFactorType>, IMultiFactorTypePersistence
    {
        public MultiFactorTypePersistence(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public override async Task<RepositoryActionResult<Entity.MultiFactorType>> AddAsync(
            Entity.MultiFactorType multiFactorType)
        {
            await using var tx = await Context.Database.BeginTransactionAsync();
            try
            {
                var lastMultiFactorType = DbSet.OrderByDescending(x => x.Code).ToArray().FirstOrDefault();
                var serial = lastMultiFactorType == null
                    ? "1".ToTwoChar()
                    : (lastMultiFactorType.Code.ToNumValue() + 1)
                    .ToNumValue().ToString(CultureInfo.InvariantCulture).ToTwoChar();
                multiFactorType.Code = serial;

                DbSet.Add(multiFactorType);
                var result = await SaveChangesAsync();
                if (result == 0)
                {
                    return new RepositoryActionResult<Entity.MultiFactorType>(multiFactorType, RepositoryActionStatus.NothingModified);
                }

                await tx.CommitAsync();
                return new RepositoryActionResult<Entity.MultiFactorType>(multiFactorType, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<Entity.MultiFactorType>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
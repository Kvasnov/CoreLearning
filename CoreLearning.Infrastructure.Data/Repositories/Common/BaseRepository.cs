using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Common;
using CoreLearning.DBLibrary.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data.Repositories.Common
{
    public abstract class BaseRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected BaseRepository(MessengerContext context)
        {
            this.context = context;
        }

        protected readonly MessengerContext context;

        public async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }
    }
}
using System.Threading.Tasks;

namespace CoreLearning.DBLibrary.Interfaces.Common
{
    public interface IEntityRepository<TEntity> where TEntity : class, new()
    {
        Task AddAsync(TEntity entity);
        Task SaveAsync();
    }
}

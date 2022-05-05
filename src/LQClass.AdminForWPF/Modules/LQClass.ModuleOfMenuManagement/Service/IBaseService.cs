using System.Threading.Tasks;

namespace MyToDo.Service;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(int id);

    Task<TEntity> GetFirstOfDefaultAsync(int id);

    Task<TEntity> GetAllAsync(TEntity parameter);
}
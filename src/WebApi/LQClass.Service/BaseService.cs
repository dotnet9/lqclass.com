using LQClass.IRepository;
using LQClass.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LQClass.Service
{
	public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
	{
		protected IBaseRepository<TEntity> baseRepository;

		public async Task<bool> CreateAsync(TEntity entity)
		{
			return await baseRepository.CreateAsync(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			return await baseRepository.DeleteAsync(id);
		}

		public async Task<bool> EditAsync(TEntity entity)
		{
			return await EditAsync(entity);
		}

		public async Task<TEntity> FindAsync(int id)
		{
			return await baseRepository.FindAsync(id);
		}

		public async Task<List<TEntity>> QueryAsync()
		{
			return await baseRepository.QueryAsync();
		}

		public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
		{
			return await baseRepository.QueryAsync(func);
		}

		public async Task<List<TEntity>> QueryAsync(int page, int size, global::SqlSugar.RefAsync<int> total)
		{
			return await baseRepository.QueryAsync(page, size, total);
		}

		public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, global::SqlSugar.RefAsync<int> total)
		{
			return await baseRepository.QueryAsync(func, page, size, total);
		}
	}
}

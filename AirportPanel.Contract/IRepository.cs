using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AirportPanel.Abstracts;


namespace AirportPanel.Contract
{
	public interface IRepository<TEntity> where TEntity : BaseModel<Guid>
	{
		void Create(params TEntity[] entities);

		void Remove(params TEntity[] entities);

		void RemoveById(Guid id);

		void Update(params TEntity[] entities);

		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> selector,
			params Expression<Func<TEntity, object>>[] includedProperties);

		TEntity GetById(Guid id, params Expression<Func<TEntity, object>>[] includedProperties);
	}

	public interface IRepositoryAsync<TEntity> where TEntity : BaseModel<Guid>
	{
		Task CreateAsync(params TEntity[] entities);

		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> selector,
		params Expression<Func<TEntity, object>>[] includedProperties);

		Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includedProperties);
	}

}

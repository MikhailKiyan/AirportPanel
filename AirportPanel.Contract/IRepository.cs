namespace AirportPanel.Contract
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using AirportPanel.Abstracts;

	public interface IRepository<TEntity>
		where TEntity : BaseModel
	{
		Task Create(params TEntity[] entities);

		Task Remove(params TEntity[] entities);

		Task Remove(Guid id);

		Task Update(params TEntity[] entities);

		Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>> selector,
			params Expression<Func<TEntity, object>>[] includedProperties);

		Task<TEntity> Get(
			Guid id,
			params Expression<Func<TEntity, object>>[] includedProperties);

		Task<IEnumerable<TEntity>> Get(
			params Expression<Func<TEntity, object>>[] includedProperties);

		Task Save();
	}
}

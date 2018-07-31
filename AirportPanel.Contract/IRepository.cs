using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

using AirportPanel.Abstracts;

namespace AirportPanel.Contract
{
	public interface IRepository<TEntity> where TEntity : BaseModel<Guid>
	{
		bool Create(params TEntity[] entities);

		bool Remove(params TEntity[] entities);

		bool RemoveById(Guid id);

		bool Update(params TEntity[] entities);

		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> selector,
			params Expression<Func<TEntity, object>>[] includedProperties);

		TEntity GetById(Guid id, params Expression<Func<TEntity, object>>[] includedProperties);
	}
}

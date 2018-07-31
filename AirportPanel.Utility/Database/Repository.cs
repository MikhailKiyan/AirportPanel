namespace AirportPanel.Utility.DB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using AirportPanel.Abstracts;
	using AirportPanel.Contract;

	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : BaseModel<Guid>
	{
		public bool Create(params TEntity[] entities) {
			throw new NotImplementedException();
		}

		public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> selector,
				params Expression<Func<TEntity, object>>[] includedProperties) {
			throw new NotImplementedException();
		}

		public TEntity GetById(Guid id, params Expression<Func<TEntity, object>>[] includedProperties) {
			throw new NotImplementedException();
		}

		public bool Remove(params TEntity[] entities) {
			throw new NotImplementedException();
		}

		public bool RemoveById(Guid id) {
			throw new NotImplementedException();
		}

		public bool Update(params TEntity[] entities) {
			throw new NotImplementedException();
		}
	}
}

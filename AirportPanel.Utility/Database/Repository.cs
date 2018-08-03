namespace AirportPanel.Utility.DB
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Microsoft.EntityFrameworkCore;

	using AirportPanel.Abstracts;
	using AirportPanel.Contract;
	using System.Linq;
	using Microsoft.EntityFrameworkCore.ChangeTracking;

	public class Repository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity>
		where TEntity : BaseModel<Guid>
	{
		protected readonly DbContext Context;

		protected readonly DbSet<TEntity> DbSet;

		public Repository(DbContext context) {
			this.Context = context;
			this.DbSet = this.Context.Set<TEntity>();
		}

		public void Create(params TEntity[] entities) {
			this.DbSet.AddRange(entities);
		}

		public async Task CreateAsync(params TEntity[] entities) {
			await this.DbSet.AddRangeAsync(entities);
		}

		public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> selector,
				params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			query = AddIncludedProperties(query, includedProperties);
			if (selector != null) {
				query = query.Where(selector);
			}
			return query.ToList();
		}

		public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> selector,
		params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			query = AddIncludedProperties(query, includedProperties);
			if (selector != null) {
				query = query.Where(selector);
			}
			return await query.ToListAsync();
		}

		public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			return AddIncludedProperties(query, includedProperties);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			query = AddIncludedProperties(query, includedProperties);
			return query;
		}

		public TEntity GetById(Guid id, params Expression<Func<TEntity, object>>[] includedProperties) {
			IEnumerable<TEntity> collection = GetAll(includedProperties);
			return collection.SingleOrDefault(item => item.Id == id);
		}

		public void Remove(params TEntity[] entities) {
			Parallel.ForEach(entities, item => {
				if (this.Context.Entry(item).State == EntityState.Detached) {
					this.DbSet.Attach(item);
				}
			});
			this.DbSet.RemoveRange(entities);
		}

		public void RemoveById(Guid id) {
			TEntity entityItem = this.DbSet.Find(id);
			this.DbSet.Remove(entityItem);
		}

		public void Update(params TEntity[] entities) {
			this.DbSet.AttachRange(entities);
			Parallel.ForEach(entities, item => {
				this.Context.Entry(item).State = EntityState.Modified;
			});
		}

		IQueryable<TEntity> AddIncludedProperties(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includedProperties) {
			if (includedProperties != null) {
				foreach (object includeProperty in includedProperties) {
					// query = query.Include(includeProperty); // TODO: Доделать - проблема со вторым типом-аргументом (object)
				}
			}
			return query;
		}
	}
}

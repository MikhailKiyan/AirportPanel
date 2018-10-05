namespace AirportPanel.Utility.DB
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using Microsoft.EntityFrameworkCore;
	using AirportPanel.Abstracts;
	using AirportPanel.Contract;

	using Microsoft.EntityFrameworkCore.ChangeTracking;

	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : BaseModel
	{
		protected readonly DbContext Context;

		protected readonly DbSet<TEntity> DbSet;

		public Repository(DbContext context) {
			this.Context = context;
			this.DbSet = this.Context.Set<TEntity>();
		}

		public async Task Create(params TEntity[] entities) {
			await this.DbSet.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> selector,
		params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			// query = await AddIncludedProperties(query, includedProperties);
			if (selector != null) {
				query = query.Where(selector);
			}
			return await query.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> Get(params Expression<Func<TEntity, object>>[] includedProperties) {
			IQueryable<TEntity> query = this.DbSet;
			//query = await AddIncludedProperties(query, includedProperties);
			return await query.ToListAsync();
		}

		public async Task<TEntity> Get(Guid id, params Expression<Func<TEntity, object>>[] includedProperties) {
			// IEnumerable<TEntity> collection = await Get(includedProperties);
			// return collection.SingleOrDefaulAsync(item => item.Id == id);
			// return await this.DbSet.FindAsync(id);
			return await this.DbSet.FirstOrDefaultAsync(item => item.Id == id);
		}

		public async Task Remove(params TEntity[] entities) {
			Parallel.ForEach(entities, item => {
				if (this.Context.Entry(item).State == EntityState.Detached) {
					this.DbSet.Attach(item);
				}
			});
			this.DbSet.RemoveRange(entities);
		}

		public async Task Remove(Guid id) {
			TEntity entity = await Get(id);
			this.DbSet.Remove(entity);
		}

		public async Task Update(params TEntity[] entities) {
			this.DbSet.AttachRange(entities);
			entities.Select(item => this.Context.Entry(item).State = EntityState.Modified);
		}

		public async Task Save() {
			await this.Context.SaveChangesAsync();
		}

		/*
		Task<IQueryable<TEntity>> AddIncludedProperties(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includedProperties) {
			if (includedProperties != null) {
				foreach (object includeProperty in includedProperties) {
					// query = query.Include(includeProperty); // TODO: Доделать - проблема со вторым типом-аргументом (object)
				}
			}
			return await query;
		}*/
	}
}

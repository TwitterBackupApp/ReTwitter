using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReTwitter.Data.Models.Abstracts;
using System;
using System.Linq;

namespace ReTwitter.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IDeletable
    {
        private readonly ReTwitterDbContext context;
        private DbSet<T> dbSet;

        public GenericRepository(ReTwitterDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("An instance of ReTwitterContext is required to use this repository.", "context");
            }

            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> All
        {
            get
            {
                return this.dbSet.AsQueryable().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.dbSet.AsQueryable();
            }
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            EntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);

            if (entry.State != EntityState.Modified)
            {
                entry.State = EntityState.Modified;
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReTwitter.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
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

        //public virtual T GetById(T id)
        //{
        //    if (id == null)
        //    {
        //        throw new ArgumentNullException("Id cannot be null");
        //    }

        //    var item = this.dbSet.Find(id);

        //    if (item == null)
        //    {
        //        throw new ArgumentNullException("No such item found");
        //    }

        //    return item;
        //}

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

        public void AddRange(IEnumerable<T> entities)
        {
            var entitiesToAdd = new List<T>();
            foreach (var entity in entities)
            {
                EntityEntry entry = this.context.Entry(entity);

                if (entry.State != EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
                else
                {
                    entitiesToAdd.Add(entity);
                }
            }
            this.dbSet.AddRange(entitiesToAdd);
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
            //else
            //{
            //    this.dbSet.Attach(entity);
            //    this.dbSet.Remove(entity);
            //}
        }

        public void Update(T entity)
        {
            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }

}

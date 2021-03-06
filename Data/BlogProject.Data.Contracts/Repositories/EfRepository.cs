﻿namespace BlogProject.Data.Contracts.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class EfRepository<T> : IRepository<T>
        where T : class
    {
        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "An instance of DbContext is required to use this repository.");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual T GetById(params object[] id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(params object[] id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
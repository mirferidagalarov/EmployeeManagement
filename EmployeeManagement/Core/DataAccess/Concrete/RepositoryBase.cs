using Core.DataAccess.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public RepositoryBase(TContext context)
        {
            Context = context;
        }
        protected TContext Context { get; }
        public TEntity Add(TEntity t)
        {
            return Context.Add(t).Entity;
        }

        public void Delete(TEntity t)
        {
            Context.Remove(t);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
          ? Context.Set<TEntity>().AsNoTracking().ToList()
          : Context.Set<TEntity>().Where(filter).AsNoTracking().ToList();
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().SingleOrDefault(filter);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public TEntity Update(TEntity t)
        {
            return Context.Update(t).Entity;
        }
    }
}

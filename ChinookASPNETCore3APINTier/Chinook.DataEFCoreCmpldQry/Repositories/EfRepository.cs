using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ChinookContext _dbContext;

        public EfRepository(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string GetKeyName(T entity)
        {
            return _dbContext.Model.FindEntityType(typeof (T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
        }

        protected bool Exists(int id) =>
            _dbContext.Set<T>().Any(e => id.Equals((T)e.GetType().GetProperty(GetKeyName(e)).GetValue(e, null)));

        public T GetById(int id) =>
            _dbContext.Set<T>().First(e => id.Equals((T)e.GetType().GetProperty(GetKeyName(e)).GetValue(e, null)));

        public virtual List<T> List() => _dbContext.Set<T>().ToList();

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> exp);

        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> list);

        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> list);

        void Save();
        void Close();
    }


    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext dbContext;

        public GenericRepository()
        {
            dbContext = DatabaseContext.GetContext();
        } 

        public  GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public  TEntity Get(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().AddOrUpdate(entity);
        }

        public void AddRange(IEnumerable<TEntity> etities)
        {
            foreach (var entity in etities)
            {
                dbContext.Set<TEntity>().Add(entity);    
            }
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                dbContext.Set<TEntity>().Remove(entity);
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Close()
        {
            dbContext.Dispose();
        }
    }
}

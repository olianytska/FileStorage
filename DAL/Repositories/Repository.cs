using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FileStorageDbContext context;
        public Repository(FileStorageDbContext context)
        {
            this.context = context;
        }
        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public void Delete(T item)
        {
            if (context.Set<T>().Find(item) != null)
                context.Set<T>().Remove(item);
            context.SaveChanges();
        }

        public IQueryable<T> GetAllItems()
        {
            return context.Set<T>();
        }

        public void Update(T item)
        {
            var i = context.Set<T>().Find(item);
            context.Entry(i).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }
}

using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DirectoryManager : IDirectoryManager
    {
        private readonly FileStorageDbContext context;
        public DirectoryManager(FileStorageDbContext context)
        {
            this.context = context;
        }

        public void Create(UserDirectory item)
        {
            context.UserDirectories.Add(item);
            context.SaveChanges();
        }

        public void Delete(UserDirectory item)
        {
            context.UserDirectories.Remove(item);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<UserDirectory> GetAllItems()
        {
            return context.UserDirectories;
        }

        public UserDirectory Find(int id)
        {
            return context.UserDirectories.Where(x => x.DirectoryId == id).FirstOrDefault();
        }
        public void Update(UserDirectory item)
        {
            var i = context.UserDirectories.Find(item.Id);
            context.Entry(i).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }
}

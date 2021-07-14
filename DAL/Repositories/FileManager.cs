using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FileManager : IFileManager
    {
        private readonly FileStorageDbContext context;
        public FileManager(FileStorageDbContext context)
        {
            this.context = context;
        }
        public void Create(File item)
        {
            context.Files.Add(item);
            context.SaveChanges();
        }

        public void Delete(File item)
        {
            if (context.Files.Find(item) != null)
                context.Files.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<File> GetAllItems()
        {
            return context.Files;
        }

        public void Update(File item)
        {
            var i = context.Files.Find(item);
            context.Entry(i).CurrentValues.SetValues(item);
            context.SaveChanges();
        }

        public File Find(string userId, string fileName)
        {
            return context.Files.FirstOrDefault(x => x.UserId == userId && x.Name == fileName);

        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}

using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        private readonly FileStorageDbContext context;
        public ClientManager(FileStorageDbContext context)
        {
            this.context = context;
        }
        public IQueryable<UserProfile> GetAllItems()
        {
            return context.UserProfiles;
        }

        public void Create(UserProfile item)
        {
            context.UserProfiles.Add(item);
            context.SaveChanges();
        }

        public void Delete(UserProfile item)
        {
            context.UserProfiles.Remove(item);
            context.SaveChanges();
        }
        public UserProfile Find(string userName)
        {
            return context.UserProfiles.Where(x => x.User.UserName.ToString() == userName).FirstOrDefault();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Update(UserProfile item)
        {
            var i = context.UserProfiles.Find(item.Id);
            context.Entry(i).CurrentValues.SetValues(item);
            context.SaveChanges();
        }

        
    }
}

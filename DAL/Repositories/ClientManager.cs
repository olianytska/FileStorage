using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IEnumerable<UserProfile> GetAllItems()
        {
            return context.UserProfiles;
        }

        public void Update(UserProfile item)
        {
            var i = context.UserProfiles.First(x => x.Id == item.Id);
            context.Entry(i).State = EntityState.Modified;
            context.SaveChanges();
        }

        public UserProfile Find(string userName)
        {
            return context.UserProfiles.FirstOrDefault(x => x.Email == userName);

        }

        public void Ban(UserProfile user)
        {
            var i = context.UserProfiles.First(x => x.Id == user.Id);
            i.IsBaned = 1;
            context.SaveChanges();
        }

        public void RemoveFromBan(UserProfile user)
        {
            var i = context.UserProfiles.First(x => x.Id == user.Id);
            i.IsBaned = 0;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public User FindById(string userId)
        {
            return context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}

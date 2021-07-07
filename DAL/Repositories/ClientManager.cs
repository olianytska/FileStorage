using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientManager : IRepository<UserProfile>
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Update(UserProfile item)
        {
            throw new NotImplementedException();
        }
    }
}

using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private FileStorageDbContext context;

        private FileStorageUserManager userManager;
        private FileStorageRoleManager roleManager;
        private IClientManager clientManager;
        private IFileManager fileManager;

        public UnitOfWork(string connectionString)
        {
            context = new FileStorageDbContext(connectionString);
            userManager = new FileStorageUserManager(new UserStore<User>(context));
            roleManager = new FileStorageRoleManager(new RoleStore<UserRole>(context));
            clientManager = new ClientManager(context);
            fileManager = new FileManager(context);
        }

        public FileStorageUserManager UserManager => userManager;

        public IClientManager ClientManager => clientManager;

        public FileStorageRoleManager RoleManager => roleManager;

        public IFileManager FileManager => fileManager;

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                    fileManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}

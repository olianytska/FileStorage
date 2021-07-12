using DAL.Entities;
using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        FileStorageUserManager UserManager { get; }
        FileStorageRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }
        IDirectoryManager DirectoryManager { get; }
        IRepository<File> FileManager { get; }

        Task SaveAsync();
    }
}

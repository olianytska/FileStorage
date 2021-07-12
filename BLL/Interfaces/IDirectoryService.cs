using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetAllItems();
        Task<int> GetDirectoryIdByOwner(string userName);
        Task CreateDirectory(string userName, string name);
        void CreateStatusForUsers(int id, string userName);
        Task<bool> IsDirectoryExist(string userName, string name);
        long GetSize(int id);
        Task RemoveDirectory(int id);
        Task DoPrivate(int id); 
    }
}

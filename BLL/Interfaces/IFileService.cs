using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFileService : IDisposable
    {
        Task<IEnumerable<FileDTO>> GetAllFiles();

        Task<IEnumerable<FileDTO>> GetTrashFiles(string userId);

        Task<FileDTO> GetFileByName(string fileName, string userId);

        Task<IEnumerable<FileDTO>> GetFileByUserName(string userName);

        Task AddFileToTrash(string userId, string fileName);

        Task RestoreFileFromTrash(string userId, string fileName);

        Task RemoveFileFromTrash(string userId, string fileName);

        Task<bool> IsFileExist(string fileName, string userId);

        Task DoFilePrivate(string userId, string fileName);

        Task RemoveFileFromPrivate(string userId, string fileName);

        Task AddFile(FileDTO item);
    }
}

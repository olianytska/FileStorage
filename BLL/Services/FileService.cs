using AutoMapper;
using BLL.DTO;
using BLL.Infrastrucrure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public FileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(x => x.AddProfile(new AutomapperProfile()));
            mapper = new Mapper(config);
        }

        public async Task AddFile(FileDTO item)
        {
            File file = new File();
            unitOfWork.FileManager.Create(mapper.Map<FileDTO, File>(item));
            await unitOfWork.SaveAsync(); 
        }

        public async Task AddFileToTrash(string userId, string fileName)
        {
            File file =  unitOfWork.FileManager.Find(userId, fileName);
            if (file == null) throw new FileStorageException();
            file.IsRemove = true;
            await unitOfWork.SaveAsync();
        }

        public async Task DoFilePrivate(string userId, string fileName)
        {
            File file =  unitOfWork.FileManager.Find(userId, fileName);
            if (file == null) throw new FileStorageException();
            file.IsPrivate = true;
            file.Link = null;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FileDTO>> GetAllFiles()
        {
            List<FileDTO> fileDTOs = new List<FileDTO>();
            foreach (var file in unitOfWork.FileManager.GetAllItems())
            {
                fileDTOs.Add(mapper.Map<File, FileDTO>(file));
            }
            return fileDTOs;
        }

        public async Task<FileDTO> GetFileByName(string fileName, string userId)
        {
            File file = unitOfWork.FileManager.Find(userId, fileName);
            if(file == null)
                throw new FileStorageException();
            return mapper.Map<File, FileDTO>(file);
        }

        public async Task<IEnumerable<FileDTO>> GetTrashFiles(string userId)
        {
            List<FileDTO> fileDTOs = new List<FileDTO>();
            foreach (var file in unitOfWork.FileManager.GetAllItems())
            {
                if(file.UserId == userId && file.IsRemove)
                fileDTOs.Add(mapper.Map<File, FileDTO>(file));
            }
            return fileDTOs;
        }

        public async Task<bool> IsFileExist(string fileName, string userId)
        {
            if ( unitOfWork.FileManager.Find(userId, fileName) == null)
                return false;
            else return true;
        }

        public async Task RemoveFileFromPrivate(string userId, string fileName)
        {
            File file =  unitOfWork.FileManager.Find(userId, fileName);
            if (file == null) throw new FileStorageException();
            file.IsPrivate = false;
            file.Link = Guid.NewGuid().ToString().Substring(2, 10);
            await unitOfWork.SaveAsync();
        }

        public  async Task RemoveFileFromTrash(string userId, string fileName)
        {
            File file =  unitOfWork.FileManager.Find(userId, fileName);
            unitOfWork.FileManager.Delete(file);
            await unitOfWork.SaveAsync();
        }

        public async Task RestoreFileFromTrash(string userId, string fileName)
        {
            File file =  unitOfWork.FileManager.Find(userId, fileName);
            file.IsRemove = false;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FileDTO>> GetFileByUserName(string userName)
        {
            List<FileDTO> fileDTOs = new List<FileDTO>();
            foreach (var file in unitOfWork.FileManager.GetAllItems())
            {
                if (file.User.UserName == userName)
                    fileDTOs.Add(mapper.Map<File, FileDTO>(file));
            }
            return fileDTOs;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}

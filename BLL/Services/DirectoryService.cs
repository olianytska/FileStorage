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
    public class DirectoryService : IDirectoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DirectoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(x => x.AddProfile(new AutomapperProfile()));
            mapper = new Mapper(config);
        }

        public async Task CreateDirectory(string userName, string name)
        {
            if (!await IsDirectoryExist(userName, name))
            {
                unitOfWork.DirectoryManager.Create(new UserDirectory
                {
                    User = mapper.Map<UserDTO, User>(mapper.Map<UserProfile, UserDTO>(unitOfWork.ClientManager.Find(userName))),
                    Directory = new Directory()
                    {
                        Name = name,
                        Size = 0,
                        Created = DateTime.Now
                    }
                });
            }
            else new FileStorageException();
        }

        public void CreateStatusForUsers(int id, string userName)
        {
            unitOfWork.DirectoryManager.Find(id).User.UserName = userName;
        }

        public void DoPrivate(int id)
        {
            unitOfWork.DirectoryManager.Find(id).Directory.IsPrivate = true;
        }

        public IEnumerable<DirectoryDTO> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetDirectoryIdByOwner(string userName)
        {
            throw new NotImplementedException();
        }

        public long GetSize(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDirectoryExist(string userName, string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveDirectory(int id)
        {
            throw new NotImplementedException();
        }
    }
}

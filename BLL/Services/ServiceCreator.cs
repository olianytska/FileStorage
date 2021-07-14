using AutoMapper;
using BLL.Infrastrucrure;
using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IFileService CreateFileService(string connection)
        {
            return new FileService(new UnitOfWork(connection));
        }

        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}

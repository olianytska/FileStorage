using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFileManager : IRepository<File>, IDisposable
    {
        File Find(string userId, string fileName);
    }
}

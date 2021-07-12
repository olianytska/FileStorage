using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDirectoryManager : IDisposable, IRepository<UserDirectory>
    {
        UserDirectory Find(int id);
    }
}

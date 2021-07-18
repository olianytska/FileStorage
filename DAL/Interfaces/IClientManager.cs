using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IClientManager : IRepository<UserProfile>, IDisposable
    {
        UserProfile Find(string userName);
        User FindById(string userId);
        void Ban(UserProfile user);
        void RemoveFromBan(UserProfile user);

    }
}

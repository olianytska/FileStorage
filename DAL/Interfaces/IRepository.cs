using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> 
    {
        void Create(T item);
        void Delete(T item);
        void Update(T item);
        IEnumerable<T> GetAllItems();
    }
}

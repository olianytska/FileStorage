using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserDirectory : BaseEntity
    {
        public int UserId { get; set; }
        public int DirectoryId { get; set; }
        public User User { get; set; }
        public Directory Directory { get; set; }
    }
}

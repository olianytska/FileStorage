using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Directory : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public string Link { get; set; }
        public DateTime Created { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsRemove { get; set; }

        public IEnumerable<UserDirectory> UserDirectories { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}

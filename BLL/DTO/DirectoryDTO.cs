using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DirectoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public string Link { get; set; }
        public DateTime Created { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsRemove { get; set; }
        public IEnumerable<string> UserName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsRemove { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}

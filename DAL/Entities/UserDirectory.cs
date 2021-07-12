using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserDirectory : BaseEntity
    {
        public enum Status
        {
            Owner, 
            Reader,
            Commentator,
            Editor
        }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Directory")]
        public int DirectoryId { get; set; }
        public User User { get; set; }
        public Directory Directory { get; set; }
    }
}

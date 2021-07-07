using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FileStorageDbContext : IdentityDbContext<User>
    {
        public FileStorageDbContext(string conectionString) : base(conectionString)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<UserDirectory> UserDirectories { get; set; }
    }
}

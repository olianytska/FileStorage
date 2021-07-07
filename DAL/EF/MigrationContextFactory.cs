using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class MigrationContextFactory : IDbContextFactory<FileStorageDbContext>
    {
        public FileStorageDbContext Create()
        {
            return new FileStorageDbContext("DefaultConnection");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastrucrure
{
    public class FileStorageException : Exception
    {
        public FileStorageException() : base()
        {
        }

        public FileStorageException(string message) 
            : base(message)
        {
        }

        public FileStorageException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected FileStorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

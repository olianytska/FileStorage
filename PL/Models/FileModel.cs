using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class FileModel
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public string Created { get; set; }

        public bool IsPrivate { get; set; }
    }
}
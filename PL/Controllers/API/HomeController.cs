using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Mvc;

namespace PL.Controllers
{
    //[EnableCors(origins: "http://localhost:44303/api/home", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        //private const long MAX_STORAGE_SIZE = 5368709120; // 5GB
        const string FILE_PATH = @"C:\Users\Liza\Desktop\";
        private IFileService FileService => HttpContext.Current.GetOwinContext().GetUserManager<IFileService>();
        private IUserService UserService => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

        [HttpPost]
        public async Task<string> Upload([FromBody] FileModel file)
        {
            if (file == null)
            {
                return "Please, upload 1 file";
            }
            try
            {
                FileDTO fileDTO = new FileDTO()
                {
                    Name = file.Name.Split('.')[0],
                    Type = file.Name.Substring(file.Name.LastIndexOf('.') + 1),
                    Size = file.Size,
                    UserName = User.Identity.Name,
                    UserId = User.Identity.GetUserId(),
                    IsPrivate = false,
                    IsRemove = false,
                    Path = FILE_PATH + file.Path,
                    Created = DateTime.Now,
                    Link = Guid.NewGuid().ToString().Substring(2, 10)
                };
                await FileService.AddFile(fileDTO);
            }
            catch (Exception)
            {
                return "Exception was cathed!";
            }

            return "File was added!";
        }
        [Route("api/home/files")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFiles()
        {
            List<FileDTO> fileDTOs = new List<FileDTO>();
            foreach (var file in await FileService.GetAllFiles())
            {
                if (file.UserId == User.Identity.GetUserId())
                {
                    fileDTOs.Add(file);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, fileDTOs);
        }
    }
}

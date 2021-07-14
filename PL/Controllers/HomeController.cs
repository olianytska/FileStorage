using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : ApiController
    {
        private const long MAX_STORAGE_SIZE = 5368709120; // 5GB
        private IFileService FileService => HttpContext.Current.GetOwinContext().GetUserManager<IFileService>();

        public async Task<HttpResponseMessage> Index()
        {
            var filesDTOs = await FileService.GetFileByUserName(User.Identity.Name);
            List<FileModel> fileModels = new List<FileModel>();
            foreach(var fileDTO in filesDTOs)
            {
                fileModels.Add(new FileModel()
                {
                    Name = fileDTO.Name,
                    Size = fileDTO.Size,
                    Created = fileDTO.Created.ToString(),
                    IsPrivate = fileDTO.IsPrivate
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, fileModels);
        }

    }
}

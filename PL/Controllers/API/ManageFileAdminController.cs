using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PL.Controllers.API
{
    [Authorize(Roles = "admin")]
    public class ManageFileAdminController : ApiController
    {
        private IFileService FileService => HttpContext.Current.GetOwinContext().GetUserManager<IFileService>();
        private IUserService UserService => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

        [Route("api/admin/allfiles")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFiles()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await FileService.GetAllFiles());
        }

        [Route("api/admin/files/{fileName}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFile(string fileName)
        {
            foreach (var file in await FileService.GetAllFiles())
            {
                if (file.Name == fileName)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, file);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/admin/AddFileToTrash")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddFileToTrash(string userId, string fileName)
        {
            if (await FileService.IsFileExist(fileName, userId))
            {
                await FileService.AddFileToTrash(userId, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/admin/RestoreFileFromTrash")]
        [HttpPut]
        public async Task<HttpResponseMessage> RestoreFileFromTrash(string userId, string fileName)
        {
            if (await FileService.IsFileExist(fileName, userId))
            {
                await FileService.RestoreFileFromTrash(userId, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/admin/Delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(string userId, string fileName)
        {
            if (await FileService.IsFileExist(fileName, userId))
            {
                await FileService.RemoveFileFromTrash(userId, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/admin/DoFilePrivate")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DoFilePrivate(string userId, string fileName)
        {
            if (await FileService.IsFileExist(fileName, userId))
            {
                await FileService.DoFilePrivate(userId, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        
        [Route("api/admin/RemoveFileFromPrivate")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveFileFromPrivate(string userId, string fileName)
        {
            if (await FileService.IsFileExist(fileName, userId))
            {
                await FileService.RemoveFileFromPrivate(userId, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}

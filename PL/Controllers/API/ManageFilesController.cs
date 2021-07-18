using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
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

namespace PL.Controllers.API
{
    public class ManageFilesController : ApiController
    {
        private IFileService FileService => HttpContext.Current.GetOwinContext().GetUserManager<IFileService>();
        private IUserService UserService => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

        [AllowAnonymous]
        [Route("{link}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Share(string link)
        {
            foreach (var file in await FileService.GetAllFiles())
            {
                if (file.Link == link && !file.IsPrivate && !file.IsRemove)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, file);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Authorize(Roles = "user, admin")]
        [Route("api/ManageFiles/files/addFileToTrash")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddFileToTrash([FromBody] FileModel fileModel)
        {
            if(await FileService.IsFileExist(fileModel.Name, User.Identity.GetUserId()))
            {
                await FileService.AddFileToTrash(User.Identity.GetUserId(), fileModel.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "File was added to trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }    
        }

        [Authorize(Roles = "user, admin")]
        [Route("api/ManageFiles/files/RestoreFileFromTrash")]
        [HttpPut]
        public async Task<HttpResponseMessage> RestoreFileFromTrash([FromBody] FileModel fileModel)
        {
            if (await FileService.IsFileExist(fileModel.Name, User.Identity.GetUserId()))
            {
                await FileService.RestoreFileFromTrash(User.Identity.GetUserId(), fileModel.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "File was restored from trash");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "user, admin")]
        [Route("api/ManageFiles/files/Delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromBody] FileModel fileModel)
        {
            if (await FileService.IsFileExist(fileModel.Name, User.Identity.GetUserId()))
            {
                await FileService.RemoveFileFromTrash(User.Identity.GetUserId(), fileModel.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "File was deleted!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "user, admin")]
        [Route("api/ManageFiles/files/DoFilePrivate")]
        [HttpPut]
        public async Task<HttpResponseMessage> DoFilePrivate([FromBody] FileModel fileModel)
        {
            if (await FileService.IsFileExist(fileModel.Name, User.Identity.GetUserId()))
            {
                await FileService.DoFilePrivate(User.Identity.GetUserId(), fileModel.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "File is private!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "user, admin")]
        [Route("api/ManageFiles/files/RemoveFileFromPrivate")]
        [HttpPut]
        public async Task<HttpResponseMessage> RemoveFileFromPrivate([FromBody] FileModel fileModel)
        {
            
                await FileService.RemoveFileFromPrivate(User.Identity.GetUserId(), fileModel.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "File is not private!");
            
        }
    }
}

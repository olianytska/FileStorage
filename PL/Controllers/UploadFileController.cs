using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UploadFileController : Controller
    {
        private IFileService FileService => HttpContext.GetOwinContext().GetUserManager<IFileService>();
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    FileDTO fileDTO = new FileDTO()
                    {
                        Name = fileName.Split('.')[0],
                        Type = file.ContentType,
                        Size = file.ContentLength,
                        UserName = User.Identity.Name,
                        UserId = User.Identity.GetUserId(),
                        IsPrivate = false,
                        IsRemove = false,
                        Path = path,
                        Created = DateTime.Now,
                        Link = Guid.NewGuid().ToString().Substring(2, 10)
                    };
                    await FileService.AddFile(fileDTO);
                    // file.SaveAs(path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch (Exception)
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}
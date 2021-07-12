using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PL.Controllers
{
    public class AccountController : ApiController
    {

        private IUserService UserService => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.Current.GetOwinContext().Authentication;

        public HttpResponseMessage Get()
        {
            //var res = new { key1 = "value1", key2 = "val2" };
           // return Request.CreateResponse(HttpStatusCode.OK, res);
            return Request.CreateResponse(HttpStatusCode.OK, UserService.GetAllItems());
        }

        [HttpPost]
        public async Task<string> CreateUser(RegisterModel model)
        {
                UserDTO userDTO = new UserDTO
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = model.Password,
                    UserName = model.Email,
                    Role = "user"
                };
                bool success = await UserService.Create(userDTO);
            if(success)
                return "User was created!";
            else
                return "Fail to create user";  
        }

    }
}

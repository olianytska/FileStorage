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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PL.Controllers
{
   
    public class AccountController : ApiController
    {

        private IUserService UserService => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.Current.GetOwinContext().Authentication;


        [HttpGet]
       // [Authorize(Roles = "admin")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserService.GetAllItems().Result);
        }

        [HttpPost]
        public async Task<string> CreateUser([FromBody] RegisterModel model)
        {
            await SetInitialDataAsync();
            UserDTO userDTO = new UserDTO
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                UserName = model.Email,
                IsBaned = false,
                Role = "user"
            };
            bool success = await UserService.Create(userDTO);
            ClaimsIdentity claim = await UserService.Authenticate(userDTO);
            if (success)
            {
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                return "Added success!";
            }
            else
                return "Failed!";
        }

        [Route("api/account/login")]
        [HttpPost]
        public async Task<string> Login([FromBody] LoginModel model)
        {
            foreach(var user in await UserService.GetAllItems())
            {
                if (user.IsBaned == true && user.Email == model.Email && user.Password == model.Password) return "User is baned!";
            }
            await SetInitialDataAsync();
            ClaimsIdentity claim = await UserService.Authenticate(new UserDTO { Email = model.Email, Password = model.Password });
            if (claim != null)
            {
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                return "Login success!";
            }
            else return "Login unsuccess!";

        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Name = "Liza",
                Surname = "Olianytska",
                Email = "olianytska@gmail.com",
                UserName = "olianytska@gmail.com",
                Password = "123456",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }

        [Route("api/account/ban/{id}")]
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<string> BanUser(string id)
        {
            foreach(var user in await UserService.GetAllItems())
            {
                if (user.Id == id)
                {
                    await UserService.Ban(user);
                    return "You ban user!";
                }
            }
            return "User not found";
        }

        [Route("api/account/removefromban/{id}")]
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<string> RemoveFromBan(string id)
        {
            foreach (var user in await UserService.GetAllItems())
            {
                if (user.Id == id)
                {
                    await UserService.RemoveFromBan(user);
                    return "You unban user!";
                }
            }
            return "User not found";
        }
    }

}

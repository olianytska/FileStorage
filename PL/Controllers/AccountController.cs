using BLL.DTO;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interfaces;
using System.Web;
using System;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (userDto.Role == "user") return RedirectToAction("Index2", "Home");
                    else if (userDto.Role == "admin") return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index2", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password,
                    Role = "user"
                };
                bool success = await UserService.Create(userDto);
                if (success)
                    Console.WriteLine("Ok");
                else
                {
                    //ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    Console.WriteLine("Not Ok");
                }
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "olianytska@gmail.com",
                Name = "Liza",
                Surname = "Olianytska",
                UserName = "olianytska@gmail.com",
                Password = "123456",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}
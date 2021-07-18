using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(PL.App_Start.Startup))]

namespace PL.App_Start
{
    public class Startup
    {
        readonly IServiceCreator serviceCreator = new ServiceCreator();
   
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.CreatePerOwinContext<IFileService>(CreateFileService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Index"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }

        private IFileService CreateFileService()
        {
            return serviceCreator.CreateFileService("DefaultConnection");
        }
    }
}

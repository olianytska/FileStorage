using AutoMapper;
using BLL.DTO;
using BLL.Infrastrucrure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(x => x.AddProfile(new AutomapperProfile()));
            mapper = new Mapper(config);
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            User user = await unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null) claim = await unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<bool> Create(UserDTO userDto)
        {
            User user = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User();
                user = mapper.Map<UserDTO, User>(userDto);
                //user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await unitOfWork.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0) return false;
                await unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                UserProfile userProfile = new UserProfile { Id = user.Id, Name = userDto.UserName };
                unitOfWork.ClientManager.Create(userProfile);
                return true;
            }
            else return false;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole { Name = roleName };
                    await unitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}

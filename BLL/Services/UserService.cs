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
using System.Threading.Tasks;

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
                UserProfile userProfile = new UserProfile
                {
                    Id = user.Id,
                    Email = userDto.Email,
                    Name = userDto.Name,
                    Surname = userDto.Surname
                };
                unitOfWork.ClientManager.Create(userProfile);
                await unitOfWork.SaveAsync();
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

        public async Task<IEnumerable<UserDTO>> GetAllItems()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var userProfile in  unitOfWork.ClientManager.GetAllItems())
            {
                userDTOs.Add(mapper.Map<UserProfile, UserDTO>(userProfile));
            }
            return userDTOs;
        }

        public async Task Delete(UserDTO userDTO)
        {
            UserProfile user = new UserProfile();
            mapper.Map<UserDTO, UserProfile>(userDTO);
            await unitOfWork.SaveAsync();
        }

        public async Task Ban(UserDTO userDTO)
        {

            UserProfile user = new UserProfile();
            user = mapper.Map<UserDTO, UserProfile>(userDTO);
            unitOfWork.ClientManager.Ban(user);
            await unitOfWork.SaveAsync();
        }

        public async Task RemoveFromBan(UserDTO userDTO)
        {

            UserProfile user = new UserProfile();
            user = mapper.Map<UserDTO, UserProfile>(userDTO);
            unitOfWork.ClientManager.RemoveFromBan(user);
            await unitOfWork.SaveAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<UserDTO>> GetAllItems();

        Task<bool> Create(UserDTO userDto);

        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        Task SetInitialData(UserDTO adminDto, List<string> roles);

        Task Delete(UserDTO userDTO);

        Task Ban(UserDTO userDTO);
        Task RemoveFromBan(UserDTO userDTO);
    }
}

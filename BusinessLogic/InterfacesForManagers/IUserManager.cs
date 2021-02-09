using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IUserManager
    {
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IReadOnlyCollection<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByNameAsync(string name);
        Task CreateUserAsync(UserDto newUserDto, string password);
        Task DeleteUserAsync(string userId);
        Task ModifyUserNameAsync(string userId, string userName);
        Task ModifyPasswordAsync(string userId, string password);
        Task ModifyNickNameAsync(string userId, string nickName);
        Task ModifyEmailAsync(string userId, string email);
    }
}

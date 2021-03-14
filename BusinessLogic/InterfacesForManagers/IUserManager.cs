using DAL.Models;
using Microsoft.AspNetCore.Identity;
using SharedLayer.DTOs;
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
        Task<IdentityResult> CreateUserAsync(User newUserDto, string password);
        Task<IdentityResult> CreateAdminAsync(UserDto newUserDto, string password);
        Task<IdentityResult> CreateOrganizerAsync(User newUserDto, string password);
        Task AddTicketToCartAsync(string userId, int ticketId);
        Task AddTicketsFromCartToBought(OrderDto orderDto);
        Task DeleteUserAsync(string userId);
        Task<UserDto> ModifyUserNameAsync(string userId, string userName);
        Task<UserDto> ModifyPasswordAsync(string userId, string password);
        Task<UserDto> ModifyNickNameAsync(string userId, string nickName);
        Task<UserDto> ModifyEmailAsync(string userId, string email);
    }
}

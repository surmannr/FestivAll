using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IUserRepository
    {
        // Lekérdezések
        Task<UserDto> GetUserById(string userId);
        Task<UserDto> GetUserByUsername(string userName);
        Task<IReadOnlyCollection<UserDto>> GetAllUsers();
        // Létrehozás
        Task CreateUser(UserDto newUser);
        // Módosítások
        Task ModifyUserName(string userId, string newUserName);
        Task ModifyNickName(string userId, string newNickName);
        Task ModifyPassword(string userId, string newPassword);
        Task ModifyEmail(string userId, string newEmail);
        Task<UserFollowedEvent> AddFollowedEvent(string userId, int eventId);
        Task<Cart> AddTicketToCart(string userId, int ticketId);
        Task<BoughtTicket> AddTicketToBoughtItems(string userId, int ticketId, int amount);
        // Törlés
        Task DeleteUser(string userId);
    }
}

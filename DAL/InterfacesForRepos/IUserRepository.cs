using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IUserRepository
    {
        // Lekérdezések
        Task<User> GetUserById(string userId);
        Task<User> GetUserByUsername(string userName);
        Task<IReadOnlyCollection<User>> GetAllUsers();
        // Létrehozás
        Task<IdentityResult> CreateUser(User newUser, string password);
        // Módosítások
        Task<User> ModifyUserName(string userId, string newUserName);
        Task<User> ModifyNickName(string userId, string newNickName);
        Task<User> ModifyPassword(string userId, string newPassword);
        Task<User> ModifyEmail(string userId, string newEmail);
        Task<UserFollowedEvent> AddFollowedEvent(string userId, int eventId);
        Task<Cart> AddTicketToCart(string userId, int ticketId);
        Task<BoughtTicket> AddTicketToBoughtItems(string userId, int ticketId, int amount);
        // Törlés
        Task DeleteUser(string userId);
    }
}

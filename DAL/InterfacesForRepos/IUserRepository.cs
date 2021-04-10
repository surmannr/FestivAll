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
        Task<IReadOnlyCollection<User>> GetUserByUsername(string userName);
        Task<IReadOnlyCollection<User>> GetAllUsers();
        Task<IReadOnlyCollection<Cart>> GetCartsByUser(string userid);
        Task<IReadOnlyCollection<string>> GetRoles();
        // Létrehozás
        Task<IdentityResult> CreateUser(User newUser, string password);
        // Módosítások
        Task<User> ModifyUserName(string userId, string newUserName);
        Task<User> ModifyNickName(string userId, string newNickName);
        Task<User> ModifyPassword(string userId, string newPassword);
        Task<User> ModifyEmail(string userId, string newEmail);
        Task<UserFollowedEvent> AddFollowedEvent(string userId, int eventId);
        Task<Cart> AddTicketToCart(string userId, int ticketId);
        Task ModifyUserCart(IList<Cart> carts);
        Task ModifyUserRole(string id, string role);
        // Törlés
        Task DeleteUser(string userId);
        Task DeleteFromCart(string userid, int ticketid);
    }
}

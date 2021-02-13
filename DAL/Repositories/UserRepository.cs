using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FestivallDb db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserRepository(FestivallDb _db, UserManager<User> _userMangager, RoleManager<IdentityRole> _roleManager)
        {
            db = _db;
            userManager = _userMangager;
            roleManager = _roleManager;
        }
        public async Task<UserFollowedEvent> AddFollowedEvent(string userId, int eventId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var _event = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
                if(_event == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
                UserFollowedEvent ufe = new UserFollowedEvent()
                {
                    Event = _event,
                    EventId = eventId,
                    User = user,
                    UserId = userId
                };
                db.UserFollowedEvents.Add(ufe);
                user.FollowedEvents.ToList().Add(ufe);
                _event.FollowedByUsers.ToList().Add(ufe);
                await db.SaveChangesAsync();
                return ufe;
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }

        public async Task<BoughtTicket> AddTicketToBoughtItems(string userId, int ticketId, int amount)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var ticket = await db.Tickets.Where(e => e.Id == ticketId).FirstOrDefaultAsync();
                if (ticket == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
                BoughtTicket bt = new BoughtTicket()
                {
                    Ticket = ticket,
                    TicketId = ticketId,
                    User = user,
                    UserId = userId,
                    Amount = amount
                };
                db.BoughtTickets.Add(bt);
                user.TicketsBought.ToList().Add(bt);
                ticket.BoughtByUsers.ToList().Add(bt);
                await db.SaveChangesAsync();
                return bt;
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }

        public async Task<Cart> AddTicketToCart(string userId, int ticketId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var ticket = await db.Tickets.Where(e => e.Id == ticketId).FirstOrDefaultAsync();
                if (ticket == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
                Cart cart = new Cart()
                {
                    Ticket = ticket,
                    TicketId = ticketId,
                    User = user,
                    UserId = userId
                };
                db.Carts.Add(cart);
                user.TicketsInCart.ToList().Add(cart);
                ticket.AddToCartByUsers.ToList().Add(cart);
                await db.SaveChangesAsync();
                return cart;
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }

        public async Task<User> CreateUser(User newUser, string password)
        {
            if(newUser==null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            var role = roleManager.FindByNameAsync(newUser.Role);
            if (role == null) await roleManager.CreateAsync(new IdentityRole { Name = newUser.Role });
            var result = await userManager.CreateAsync(newUser, password);
            await userManager.AddToRoleAsync(newUser, newUser.Role);
            if (result.Succeeded)
            {
                await db.SaveChangesAsync();
                return newUser;
            }
            else throw new ApplicationException("Nem sikerült létrehozni a felhasználót.");
        }

        public async Task DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<User>> GetAllUsers()
        {
            return await db.Users.GetUsersList();
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user==null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            return user;
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            return user;

        }

        public async Task<User> ModifyEmail(string userId, string newEmail)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            if(UserRepositoryExtension.ValidateEmail(newEmail))
            {
                user.Email = newEmail;
                await db.SaveChangesAsync();
                return user;
            }
            throw new FormatException("Nem megfelelő formátumú email.");
        }

        public async Task<User> ModifyNickName(string userId, string newNickName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            if (!String.IsNullOrEmpty(newNickName))
            {
                user.NickName = newNickName;
                await db.SaveChangesAsync();
                return user;
            }
            else throw new ArgumentNullException(ExceptionMessageConstants.NullObject);
        }

        public async Task<User> ModifyPassword(string userId, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            bool valid = await UserRepositoryExtension.ValidatePassword(newPassword, userManager, user);
            if (valid)
            {
                await userManager.AddPasswordAsync(user, newPassword);
                await db.SaveChangesAsync();
                return user;
            }
            throw new FormatException("Nem megfelelő a jelszó.");
        }

        public async Task<User> ModifyUserName(string userId, string newUserName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            if(String.IsNullOrEmpty(newUserName)) throw new ArgumentNullException(ExceptionMessageConstants.NullObject);
            await userManager.SetUserNameAsync(user, newUserName);
            await db.SaveChangesAsync();
            return user;
        }
    }
    internal static class UserRepositoryExtension
    {
        public static async Task<IReadOnlyCollection<User>> GetUsersList(this IQueryable<User> users)
            => await users.ToListAsync();

        public static bool ValidateEmail(string email)
        {
            MailAddress address = new MailAddress(email);
            return (address.Address == email);
        }

        public async static Task<bool> ValidatePassword(string password, UserManager<User> userManager, User user)
        {
            var passwordValidator = new PasswordValidator<User>();
            var valid = await passwordValidator.ValidateAsync(userManager, user, password);
            return (valid.Succeeded);
        }
    }
}

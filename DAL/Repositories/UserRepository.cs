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
using SharedLayer.Exceptions;

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
                if(_event == null)
                    throw new DbModelNullException(ExceptionMessageConstants.NullObject);
                UserFollowedEvent ufe = new UserFollowedEvent()
                {
                    Event = _event,
                    EventId = eventId,
                    User = user,
                    UserId = userId
                };
                db.UserFollowedEvents.Add(ufe);
                await db.SaveChangesAsync();
                return ufe;
            }
            else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
        }

        public async Task DeleteFromCart(string userid, int ticketid)
        {
            var cart = await db.Carts.Where(c => c.TicketId == ticketid && c.UserId == userid).FirstOrDefaultAsync();
            if (cart == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            db.Carts.Remove(cart);
            await db.SaveChangesAsync();
        }
        public async Task<Cart> AddTicketToCart(string userId, int ticketId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var ticket = await db.Tickets.Where(e => e.Id == ticketId).Include(e => e.Event).FirstOrDefaultAsync();
                if (ticket == null)
                    throw new DbModelNullException(ExceptionMessageConstants.NullObject);

                var existcart = await db.Carts.Where(c => c.TicketId == ticketId && c.UserId == userId).Include(e => e.Ticket).ThenInclude(e => e.Event).FirstOrDefaultAsync();
                Cart cart = new Cart();
                if (existcart == null)
                {
                    cart = new Cart()
                    {
                        Ticket = ticket,
                        TicketId = ticketId,
                        User = user,
                        UserId = userId,
                        Amount = 1,
                        EventLocation = ticket.Event.Location,
                        EventStartDate = ticket.Event.StartDate
                    };
                    db.Carts.Add(cart);
                }
                else existcart.Amount++;
                await db.SaveChangesAsync();
                return cart;
            }
            else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<Cart>> GetCartsByUser(string userid)
        {
            return await db.Carts.Where(c => c.UserId == userid).Include(c => c.Ticket).ToListAsync();
        }

        public async Task<IReadOnlyCollection<string>> GetRoles()
        {
            return await roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        public async Task<IdentityResult> CreateUser(User newUser, string password)
        {
            if(newUser==null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            var role = await roleManager.FindByNameAsync(newUser.Role);
            if (role == null) await roleManager.CreateAsync(new IdentityRole { Name = newUser.Role });
            var result = await userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
                throw new DbUserCreationFailedException("A felhasználó létrehozása nem sikerült.");
            await userManager.AddToRoleAsync(newUser, newUser.Role);
            return result;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
            else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<User>> GetAllUsers()
        {
            return await db.Users.GetUsersList();
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user==null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            return user;
        }

        public async Task<IReadOnlyCollection<User>> GetUserByUsername(string userName)
        {
            var user = await db.Users.Where(u => u.UserName.ToUpper().Contains(userName.ToUpper()) || u.NickName.ToUpper().Contains( userName.ToUpper())).ToListAsync();
            if (user == null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            return user;

        }

        public async Task<User> ModifyEmail(string userId, string newEmail)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if(UserRepositoryExtension.ValidateEmail(newEmail))
            {
                user.Email = newEmail;
                await db.SaveChangesAsync();
                return user;
            }
            throw new DbModelParamFormatException("Nem megfelelő formátumú email.");
        }

        public async Task<User> ModifyNickName(string userId, string newNickName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if (!String.IsNullOrEmpty(newNickName))
            {
                user.NickName = newNickName;
                await db.SaveChangesAsync();
                return user;
            }
            else throw new DbModelParamsNullException(ExceptionMessageConstants.NullObject);
        }

        public async Task<User> ModifyPassword(string userId, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            bool valid = await UserRepositoryExtension.ValidatePassword(newPassword, userManager, user);
            if (valid)
            {
                await userManager.AddPasswordAsync(user, newPassword);
                await db.SaveChangesAsync();
                return user;
            }
            throw new DbModelParamFormatException("Nem megfelelő a jelszó.");
        }

        public async Task<User> ModifyUserName(string userId, string newUserName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if(String.IsNullOrEmpty(newUserName))
                throw new DbModelParamsNullException(ExceptionMessageConstants.NullObject);
            await userManager.SetUserNameAsync(user, newUserName);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task ModifyUserCart(IList<Cart> carts)
        {
            foreach(var c in carts)
            {
                var editcart = db.Carts.Where(ca => ca.TicketId == c.TicketId && ca.UserId == c.UserId).FirstOrDefault();
                if(editcart != null)
                {
                    editcart.Amount = c.Amount;
                }
            }
            await db.SaveChangesAsync();
        }

        public async Task ModifyUserRole(string id, string role)
        {
            var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(user != null)
            {
                await userManager.RemoveFromRoleAsync(user, user.Role);
                user.Role = role;
                await userManager.AddToRoleAsync(user, role);
                await db.SaveChangesAsync();
            } else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
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

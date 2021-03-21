using AutoMapper;
using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserManager(IUserRepository _userRepository, IMapper _mapper)
        {
            userRepository = _userRepository;
            mapper = _mapper;
        }

        // Lekérdezések 

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userRepository.GetUserById(userId);
            return mapper.Map<UserDto>(user);
        }

        public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync()
        {
            var users = await userRepository.GetAllUsers();
            return mapper.Map<List<UserDto>>(users);
        }

        public async Task<IReadOnlyCollection<UserDto>> GetUserByNameAsync(string name)
        {
            var user = await userRepository.GetUserByUsername(name);
            return mapper.Map<List<UserDto>>(user);
        }

        public async Task<IReadOnlyCollection<CartDto>> GetCartsByUser(string userid)
        {
            var carts = await userRepository.GetCartsByUser(userid);
            var mappedcarts = mapper.Map<List<CartDto>>(carts);
            var zip = carts.Zip(mappedcarts, (t, dt) => new { Cart = t, CartDto = dt });
            foreach (var z in zip)
            {
                z.CartDto.EventName = z.Cart.Ticket.EventName;
                z.CartDto.TicketPrice = z.Cart.Ticket.Price;
                z.CartDto.TicketCategory = z.Cart.Ticket.Category;
            }
            return mappedcarts;
        }
        // Létrehozás
        public async Task AddFollowedEventToUser(int eventid, string userid)
        {
            await userRepository.AddFollowedEvent(userid, eventid);
        }
        public async Task<IdentityResult> CreateUserAsync(User newUserDto, string password)
        {
            newUserDto.Role = "User";
            var result = await CreateWithNormalModel(newUserDto, password);
            return result;
        }

        public async Task<IdentityResult> CreateAdminAsync(UserDto newUserDto, string password)
        {
            newUserDto.Role = "Admin";
            var result = await Create(newUserDto, password);
            return result;
        }

        public async Task<IdentityResult> CreateOrganizerAsync(User newUserDto, string password)
        {
            newUserDto.Role = "Organizer";
            var result = await CreateWithNormalModel(newUserDto, password);
            return result;
        }

        public async Task<IdentityResult> Create(UserDto newUserDto, string password)
        {
            User user = mapper.Map<User>(newUserDto);
            var result = await userRepository.CreateUser(user, password);
            return result;
        }

        public async Task<IdentityResult> CreateWithNormalModel(User newUser, string password)
        {
            var result = await userRepository.CreateUser(newUser, password);
            return result;
        }

        public async Task AddTicketToCartAsync(string userId, int ticketId)
        {
            await userRepository.AddTicketToCart(userId, ticketId);
        }
        public async Task AddTicketsFromCartToBought(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);
            await userRepository.AddTicketsFromCartToBoughtItems(order);
        }

        // Törlés

        public async Task DeleteUserAsync(string userId)
            => await userRepository.DeleteUser(userId);

        public async Task DeleteTicketFromCart(string userid, int ticketid)
            => await userRepository.DeleteFromCart(userid, ticketid);

        // Módosítások

        public async Task<UserDto> ModifyUserNameAsync(string userId, string userName)
        {
            var result = await userRepository.ModifyUserName(userId, userName);
            return mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> ModifyPasswordAsync(string userId, string password)
        {
            var result = await userRepository.ModifyPassword(userId, password);
            return mapper.Map<UserDto>(result);
        }
        
        public async Task<UserDto> ModifyNickNameAsync(string userId, string nickName)
        {
            var result = await userRepository.ModifyNickName(userId, nickName);
            return mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> ModifyEmailAsync(string userId, string email)
        {
            var result = await userRepository.ModifyEmail(userId, email);
            return mapper.Map<UserDto>(result);
        }

    }
}

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

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            var user = await userRepository.GetUserByUsername(name);
            return mapper.Map<UserDto>(user);
        }

        // Létrehozás

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

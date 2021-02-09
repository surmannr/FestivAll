using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
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

        public UserManager(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        // Lekérdezések 

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userRepository.GetUserById(userId);
            return new UserDto(user.UserName, user.PasswordHash, user.Email, user.Role, user.NickName);
        }

        public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync()
        {
            var users = await userRepository.GetAllUsers();
            return users.Select(u => new UserDto(u.UserName, u.PasswordHash, u.Email, u.Role, u.NickName)).ToList();
        }

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            var user = await userRepository.GetUserByUsername(name);
            return new UserDto(user.UserName, user.PasswordHash, user.Email, user.Role, user.NickName);
        }

        // Létrehozás

        public async Task CreateUserAsync(UserDto newUserDto, string password)
        {
            User user = new User()
            {
                UserName = newUserDto.UserName,
                Email = newUserDto.Email,
                Role = newUserDto.Role,
                NickName = newUserDto.NickName
            };
            await userRepository.CreateUser(user, password);
        }

        // Törlés

        public async Task DeleteUserAsync(string userId)
            => await userRepository.DeleteUser(userId);

        // Módosítások

        public async Task ModifyUserNameAsync(string userId, string userName)
            => await userRepository.ModifyUserName(userId, userName);

        public async Task ModifyPasswordAsync(string userId, string password)
            => await userRepository.ModifyPassword(userId, password);
        
        public async Task ModifyNickNameAsync(string userId, string nickName)
            => await userRepository.ModifyNickName(userId, nickName);

        public async Task ModifyEmailAsync(string userId, string email)
            => await userRepository.ModifyEmail(userId, email);
    }
}

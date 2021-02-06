using DAL.DTOs;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class UserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        // Lekérdezések 

        public async Task<UserDto> GetUserByIdAsync(string userId)
            => await userRepository.GetUserById(userId);

        public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync()
            => await userRepository.GetAllUsers();

        public async Task<UserDto> GetUserByNameAsync(string name)
            => await userRepository.GetUserByUsername(name);

        // Létrehozás

        public async Task CreateUserAsync(UserDto newUserDto)
            => await userRepository.CreateUser(newUserDto);

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

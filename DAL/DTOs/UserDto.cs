using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class UserDto
    {
        public readonly string UserName;
        public readonly string Password;
        public readonly string Email;
        public readonly string Role;
        public readonly string NickName;

        public UserDto(string username, string password, string email, string role, string nickname)
        {
            UserName = username;
            Password = password;
            Email = email;
            Role = role;
            NickName = nickname;
        }
    }
}

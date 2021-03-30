using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string NickName { get; set; }

        public UserDto() { }

        public UserDto(string username, string password, string email, string role, string nickname)
        {
            UserName = username;
            Password = password;
            Email = email;
            Role = role;
            NickName = nickname;
        }

        public UserDto(string username, string password, string email, IList<string> role, string nickname)
        {
            UserName = username;
            Password = password;
            Email = email;
            Role = role[0];
            NickName = nickname;
        }
    }
}
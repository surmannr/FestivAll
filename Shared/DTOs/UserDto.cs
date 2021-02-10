using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class UserDto
    {
        [JsonPropertyName("username")]
        public string UserName;
        [JsonPropertyName("password")]
        public string Password;
        [JsonPropertyName("email")]
        public string Email;
        [JsonPropertyName("role")]
        public string Role;
        [JsonPropertyName("nickname")]
        public string NickName;

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

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("nickname")]
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
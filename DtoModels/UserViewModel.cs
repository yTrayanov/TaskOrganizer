using System;

namespace DtoModels
{
    public class UserViewModel
    {

        public UserViewModel(string id , string username , string role = null)
        {
            this.Id = id;
            this.Username = username;
            this.Role = role;
        }

        public string Id { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }

    }
}

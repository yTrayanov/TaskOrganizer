namespace DbModels
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {

        public User()
        {
            this.Tasks = new List<Task>();
            this.Groups = new List<UserGroup>();
            this.Messages = new List<Message>();
        }

        public string? Department { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserGroup> Groups { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}

namespace DbModels
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {

        public User()
        {
            this.Tasks = new List<UserTask>();
            this.Groups = new List<UserGroup>();
            this.Messages = new List<Message>();
        }

        public string? Department { get; set; }

        public ICollection<UserTask> Tasks { get; set; }
        public ICollection<UserGroup> Groups { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}

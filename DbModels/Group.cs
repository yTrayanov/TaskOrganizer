namespace DbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Utilities;

    public class Group
    {

        public Group()
        {
            this.Messages = new List<Message>();
            this.Users = new List<UserGroup>();
            this.Tasks = new List<Task>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(Constants.MinGroupNameLength)]
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserGroup> Users { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}

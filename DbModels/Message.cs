namespace DbModels
{
    using System.ComponentModel.DataAnnotations;
    using Utilities;
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(Constants.MinMessageLength)]
        public string Content { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }


    }
}

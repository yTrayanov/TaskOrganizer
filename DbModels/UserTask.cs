namespace DbModels
{
    using System.ComponentModel.DataAnnotations;
    public class UserTask
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}

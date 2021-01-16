namespace DbModels
{
    using System.ComponentModel.DataAnnotations;
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public Group GivenToGroup { get; set; }
        public int? GroupId { get; set; }

        [Required]
        public string Content { get; set; }

        public bool isCompleted { get; set; }

        [Required]
        public string Type { get; set; }

        [Range(1,2)]
        public int LevelOfImportance { get; set; }
    }
}

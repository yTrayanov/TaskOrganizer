using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class IndividualGroupTaskViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string GroupName { get; set; }

        public bool IsCompleted { get; set; }

        public int Level { get; set; }
    }
}

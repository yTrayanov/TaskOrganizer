using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class TasksGroupViewModel
    {
        public int GroupId { get; set; }
        public List<TaskAbs> Tasks { get; set; }
    }
}

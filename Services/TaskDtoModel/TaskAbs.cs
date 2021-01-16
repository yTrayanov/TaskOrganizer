using DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.TaskDtoModel
{
    public class TaskAbs
    {
        protected int levelOfImportance;
        public TaskAbs(Task task)
        {
            this.Id = task.Id;
            this.Content = task.Content;
            this.SetTaskToGroupOrUser(task.GivenToGroup);
            this.IsCompleted = task.isCompleted;
            this.LevelOfImportance = task.LevelOfImportance;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public Group GivenToGroup { get; set; }

        public bool IsCompleted { get; set; }

        public int LevelOfImportance
        {
            get { return this.levelOfImportance; }
            protected set
            {
                if (value < 1)
                    this.levelOfImportance = 1;
                else if (value > 9)
                    this.levelOfImportance = 9;
                else
                this.levelOfImportance = value;
            }
        }


        public virtual void SetTaskToGroupOrUser(Group group)
        {
            this.GivenToGroup = group;
        }
    }
}

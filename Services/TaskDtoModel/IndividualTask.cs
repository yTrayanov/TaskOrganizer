using DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.TaskDtoModel
{
    public class IndividualTask : TaskAbs
    {
        public IndividualTask(Task task)
            : base(task)
        {
        }

        public User? User { get; set; }

        public override void SetTaskToGroupOrUser(Group group)
        {
            if (group != null)
                if (group.Users.Count == 1)
                {
                    this.User = group.Users.FirstOrDefault().User;
                    base.SetTaskToGroupOrUser(group);
                }
                else
                {
                    throw new ArgumentException("The task is individual , but got a group");
                }
        }
    }
}

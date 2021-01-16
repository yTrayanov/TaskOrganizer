using DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.TaskDtoModel
{
    public class GroupTask : TaskAbs
    {
        public GroupTask(Task task) : base(task)
        {
        }

        public override void SetTaskToGroupOrUser(Group group)
        {
            if (group != null)
                if (group.Users.Count > 1)
                {
                    base.SetTaskToGroupOrUser(group);
                }
                else
                {
                    throw new ArgumentException("There are not enought users for this task");
                }
        }

    }
}

namespace Services
{
    using DataContext;
    using DbModels;
    using Services.TaskDtoModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    public class TaskService : Service
    {
        private ICollection<IndividualTask> individualTasks = new List<IndividualTask>();
        private ICollection<GroupTask> groupTasks = new List<GroupTask>();
        public TaskService(OrganizerDbContext context)
            : base(context)
        {
            mapTasksFromDatabaseToDtoModels();
        }

        private Task GetTaskById(int id)
        {
            return this.Context.Tasks.FirstOrDefault(t => t.Id == id);
        }


        private void mapTasksFromDatabaseToDtoModels()
        {
            foreach (var task in Context.Tasks)
            {
                if (task.Type == Constants.IndividualTaskType)
                {
                    TaskAbs individualTask = new IndividualTask(task);
                    individualTasks.Add((IndividualTask)individualTask);
                }
                else
                {
                    TaskAbs groupTask = new GroupTask(task);
                    groupTasks.Add((GroupTask)groupTask);
                }
            }
        }

        public Task CreateTask(string content, string type, int levelOfImportance)
        {
            var newTask = new Task()
            {
                Content = content,
                Type = type,
                LevelOfImportance = levelOfImportance,
                isCompleted = false
            };

            this.Context.Tasks.Add(newTask);
            this.Context.SaveChanges();

            return newTask;
        }

        public TaskAbs FindTaskById(int id, string type)
        {
            TaskAbs task;
            if (type == Constants.IndividualTaskType)
                task = this.individualTasks.FirstOrDefault(t => t.Id == id);
            else
                task = this.groupTasks.FirstOrDefault(t => t.Id == id);

            return task;
        }


        public bool DeleteTaskById(int id)
        {
            var task = GetTaskById(id);
            if (task == null)
            {
                return false;
            }

            this.Context.Tasks.Remove(task);
            this.Context.SaveChanges();

            return true;

        }

        public ICollection<IndividualTask> GetAllIndividualTasks()
        {
            return this.individualTasks;
        }

        public ICollection<GroupTask> GetAllGroupTasks()
        {
            return this.groupTasks;
        }

        public ICollection<IndividualTask> GetUserTasks(string id)
        {

            return this.individualTasks.Where(it => it.User.Id == id).ToList();
        }

        public ICollection<GroupTask> GetGroupTasks(int id)
        {

            return this.groupTasks.Where(g => g.GivenToGroup.Id == id).ToList();
        }

        public void GiveTaskToUser(int taskId, User user)
        {
            var task = this.Context.Tasks.FirstOrDefault(t => t.Id == taskId);


        }

    }
}

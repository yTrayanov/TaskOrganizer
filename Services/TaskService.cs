namespace Services
{
    using DataContext;
    using DbModels;
    using DtoModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    public class TaskService : Service
    {
        public TaskService(OrganizerDbContext context) : base(context)
        {
        }

        public Task CreateTask(TaskAbs task)
        {
            var newTask = new Task()
            {
                Content = task.Content,
                LevelOfImportance = task.LevelOfImportance,
                Type = task.Type,
                isCompleted = false
            };

            this.Context.Tasks.Add(newTask);
            this.Context.SaveChanges();

            return newTask;
        }

        public List<Task> GetUntakenIndividualTasks()
        {
            var tasks = this.Context.Tasks
                .Where(t => t.User == null &&
                    t.Type == Constants.IndividualTaskType &&
                    t.isCompleted == false)
                .ToList();

            return tasks;
        }

        public List<Task> GetUntakenGroupTasks()
        {
            var tasks = this.Context.Tasks
                .Where(t => t.Group == null &&
                    t.Type == Constants.GroupTaskType &&
                    t.isCompleted == false)
                .ToList();

            return tasks;
        }

        public void GiveTaskToUser(string userId , int taskId)
        {
            var user = this.Context.Users.FirstOrDefault(u => u.Id == userId);
            var task = this.Context.Tasks.FirstOrDefault(t => t.Id == taskId);

            user.Tasks.Add(task);
            this.Context.SaveChanges();
        }

        public List<User> GetAllUsersWhoDontHaveThisTask(int taskId)
        {
            var users = this.Context.Users
                .Include(u => u.Tasks)
                .Where(u => 
                    !u.Tasks.Any(t => t.Id == taskId) && u.UserName != Constants.AdminUsername)
                .ToList();

            return users;
        }

        public void TakeTaskForGroup(int groupId , int taskId)
        {
            var task = this.Context.Tasks.FirstOrDefault(t => t.Id == taskId);
            var group = this.Context.Groups.FirstOrDefault(g => g.Id == groupId);

            group.Tasks.Add(task);
            this.Context.SaveChanges();
        }

        public Task FindTaskById(int taskId)
        {
            return this.Context.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public Task EditTask(TaskAbs editedTask)
        {
            var task = this.FindTaskById(editedTask.Id);

            task.Content = editedTask.Content;
            task.LevelOfImportance = editedTask.LevelOfImportance;
            task.Type = editedTask.Type;

            this.Context.SaveChanges();
            return task;

        }

        public List<Task> GetCurrentGroupTasks(int groupId)
        {
            var tasks = this.Context.Tasks
                .Where(t => t.Type == Constants.GroupTaskType
                    && t.GroupId == groupId
                    && !t.isCompleted)
                .ToList();

            return tasks;
        }

        public void CompleteTask(int taskId)
        {
            var task = this.FindTaskById(taskId);
            task.isCompleted = true;
            this.Context.SaveChanges();
        }

        public List<Task> GetCurrentUserTasks(User user)
        {
            var tasks = this.Context.Tasks
                .Where(t => t.Type == Constants.IndividualTaskType && t.User == user && !t.isCompleted)
                .ToList();

            return tasks;
        }

        public List<Task> GetAllTasks()
        {
            return this.Context.Tasks
                .OrderByDescending(t => t.LevelOfImportance)
                .Include(t => t.User)
                .Include(t => t.Group)
                .ToList();
        }
    }
}

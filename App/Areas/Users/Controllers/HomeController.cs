using DbModels;
using DtoModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Areas.Users.Controllers
{
    public class HomeController : UserController
    {
        private GroupService groupService;
        private TaskService taskService;
        public HomeController(UserManager<User> userManager , GroupService groupService , TaskService taskService) : base(userManager)
        {
            this.groupService = groupService;
            this.taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<IActionResult> GetUserGroups()
        {
            var user = await this.UserManager.GetUserAsync(User);
            var groups = this.groupService.GetUserGroups(user);
            var groupViews = new List<GroupViewModel>();

            foreach (var group in groups)
            {
                var groupView = new GroupViewModel(group.Id, group.Name);
                groupViews.Add(groupView);
            }


            return View(groupViews);
        }

        public IActionResult Chat(int groupId)
        {
            var messages = this.groupService.GetGroupMessages(groupId);
            var messagesViews = new List<MessageViewModel>();


            foreach (var message in messages)
            {
                bool messageIsFromUser = this.User.Identity.Name == message.Sender.UserName ? true : false;
                var messageView = new MessageViewModel(message.Sender.UserName, message.Content, messageIsFromUser);
                messagesViews.Add(messageView);
            }

            var chatView = new ChatViewBindingModel()
            {
                GroupId = groupId,
                MessageViews = messagesViews,
            };

            return View(chatView);

        }


        public IActionResult CurrentGroupTasks(int groupId)
        {
            var tasks = this.taskService.GetCurrentGroupTasks(groupId);

            var taskViews = MapGroupTasks(tasks);

            var viewModel = new TasksGroupViewModel()
            {
                GroupId = groupId,
                Tasks = taskViews
            };


            return View(viewModel);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Chat(ChatViewBindingModel model)
        {
            var user = await this.UserManager.GetUserAsync(User);
            this.groupService.CreateMessage(model.GroupId, user, model.Content);

            return RedirectToAction("Chat", "Home", new { groupId = model.GroupId, area = "Users" });
        }

        public async System.Threading.Tasks.Task<IActionResult> GetUserTasks()
        {
            var user = await this.UserManager.GetUserAsync(User);
            var tasks = this.taskService.GetCurrentUserTasks(user);

            var taskViews = new List<TaskAbs>();

            foreach (var task in tasks)
            {
                TaskAbs taskView = new IndividualTask
                {
                    Id = task.Id,
                    LevelOfImportance = task.LevelOfImportance,
                    Content = task.Content,
                    Type = task.Type,
                    IsCompleted = task.isCompleted
                };
                taskViews.Add(taskView);
            }

            return View(taskViews);
        }

        public IActionResult CompleteTask(int taskId)
        {
            this.taskService.CompleteTask(taskId);

            return RedirectToAction("GetUserTasks", "Home", new { area = "Users"});
        }

        private static List<TaskAbs> MapGroupTasks(List<Task> tasks)
        {
            var taskViews = new List<TaskAbs>();

            foreach (var task in tasks)
            {
                var taskView = new GroupTask
                {
                    Id = task.Id,
                    LevelOfImportance = task.LevelOfImportance,
                    Content = task.Content,
                    Type = task.Type,
                    IsCompleted = task.isCompleted
                };
                taskViews.Add(taskView);
            }

            return taskViews;
        }



    }
}

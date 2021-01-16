namespace App.Areas.Teamleader.Controllers
{
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Identity;
    using DbModels;
    using DtoModels;
    using System.Collections.Generic;
    using Utilities;

    public class TaskController : TeamleaderController
    {
        private TaskService taskService;
        private GroupService groupService;
        public TaskController(UserManager<User> userManager , TaskService taskService , GroupService groupService ) : base(userManager)
        {
            this.taskService = taskService;
            this.groupService = groupService;
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public IActionResult Create(TaskAbs task)
        {
            this.taskService.CreateTask(task);

            return RedirectToAction("CreateTask" , "Task" , new { area="Teamleader"});
        }

        public IActionResult IndividualTasks()
        {
            var tasks = this.taskService.GetUntakenIndividualTasks();

            var taskViews = new List<TaskAbs>();

            foreach(var task in tasks)
            {
                TaskAbs taskView = new IndividualTask
                {
                    Id= task.Id,
                    LevelOfImportance = task.LevelOfImportance,
                    Content = task.Content,
                    Type = task.Type,
                    IsCompleted = task.isCompleted
                };
                taskViews.Add(taskView);
            }

            return View(taskViews);

        }

        public IActionResult GroupTasks()
        {
            var tasks = this.taskService.GetUntakenGroupTasks();
            List<TaskAbs> taskViews = MapGroupTasks(tasks);

            return View(taskViews);

        }


        public async System.Threading.Tasks.Task<IActionResult> GiveIndividual(int taskId)
        {
            var users = this.taskService.GetAllUsersWhoDontHaveThisTask(taskId);

            var usersViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userRoles = await this.UserManager.GetRolesAsync(user);
                if (userRoles.Contains(Constants.TeamLeader))
                {
                    continue;
                }
                var userView = new UserViewModel(user.Id, user.UserName);
                usersViews.Add(userView);
            }

            var viewModel = new AddRemoveUserViewBindingModel()
            {
                ExtraId = taskId,
                UserViews = usersViews
            };

            return View(viewModel);
        }

        public async System.Threading.Tasks.Task<IActionResult> TakeForGroup(int taskId)
        {
            var currentUser = await this.UserManager.GetUserAsync(User);
            var groups = this.groupService.GetUserGroups(currentUser);
            var groupViews = new List<GroupViewModel>();

            foreach(var group in groups)
            {
                var groupView = new GroupViewModel(group.Id, group.Name);
                groupViews.Add(groupView);
            }

            var groupTaskViewModel = new GroupTaskViewModel
            {
                TaskId = taskId,
                GroupViewModels = groupViews
            };

            

            return View(groupTaskViewModel);
        }


        public IActionResult GiveTaskToUser(string userId , int taskId)
        {
            this.taskService.GiveTaskToUser(userId, taskId);

            return RedirectToAction("IndividualTasks", "Tasks", new { area = "Teamleader"});
        }
        public IActionResult GiveTaskToGroup(int groupId, int taskId)
        {
            this.taskService.TakeTaskForGroup(groupId, taskId);

            return RedirectToAction("GroupTasks", "Task", new { area = "Teamleader" });
        }

        public IActionResult Edit(int taskId)
        {
            var task = this.taskService.FindTaskById(taskId);

            var taskView = new TaskAbs()
            {
                Id = taskId,
                Content = task.Content,
                LevelOfImportance = task.LevelOfImportance,
                Type = task.Type
            };

            return View(taskView);
        }

        public IActionResult EditTask(TaskAbs task , int taskId)
        {
            task.Id = taskId;

            this.taskService.EditTask(task);

            if(task.Type == Constants.IndividualTaskType)
            {
                return RedirectToAction("IndividualTasks", "Task", new { area = "Teamleader" });
            }


            return RedirectToAction("GroupTasks", "Task", new { area = "Teamleader" });
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

        public IActionResult CompleteTask(int taskId , int groupId)
        {
            this.taskService.CompleteTask(taskId);

            return RedirectToAction("CurrentGroupTasks", "Task", new { area = "Teamleader" , groupId = groupId });
        }

        public IActionResult GetAllTasks()
        {
            var tasks = this.taskService.GetAllTasks();
            var taskViews = new List<IndividualGroupTaskViewModel>();

            foreach (var task in tasks)
            {
                var taskView = new IndividualGroupTaskViewModel
                {
                    Id = task.Id,
                    Level = task.LevelOfImportance,
                    Content = task.Content,
                    Type = task.Type,
                    IsCompleted = task.isCompleted,
                };

                if(task.User != null)
                {
                    taskView.Username = task.User.UserName;
                    
                }
                else if(task.Group != null)
                {
                    taskView.GroupName = task.Group.Name;
                }

                taskViews.Add(taskView);
            }


            return View(taskViews);
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

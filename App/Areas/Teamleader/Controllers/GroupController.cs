namespace App.Areas.Teamleader.Controllers
{
    using DataContext;
    using DbModels;
    using DtoModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Collections.Generic;
    using Utilities;

    public class GroupController : TeamleaderController
    {
        private GroupService groupService;
        public GroupController(UserManager<User> userManager ,GroupService groupService) : base(userManager)
        {
            this.groupService = groupService;
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            var user = await this.UserManager.GetUserAsync(User);
            var groups = this.groupService.GetUserGroups(user);
            var groupViews = new List<GroupViewModel>();

            foreach(var group in groups)
            {
                var groupView = new GroupViewModel(group.Id, group.Name);
                groupViews.Add(groupView);
            }


            return View(groupViews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Create(string name)
        {
            var user = await this.UserManager.GetUserAsync(User);
            var group = this.groupService.CreateGroup(name ,user);


            return RedirectToAction("Index" , "Group" , new {area="Teamleader" });
        }
        
        public IActionResult Chat(int groupId)
        {
            var messages = this.groupService.GetGroupMessages(groupId);
            var messagesViews = new List<MessageViewModel>();


            foreach(var message in messages)
            {
                bool messageIsFromUser = this.User.Identity.Name == message.Sender.UserName ? true : false;
                var messageView = new MessageViewModel(message.Sender.UserName, message.Content , messageIsFromUser);
                messagesViews.Add(messageView);
            }

            var chatView = new ChatViewBindingModel()
            {
                GroupId = groupId,
                MessageViews = messagesViews,
            };

            return View(chatView);

        }


        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Chat(ChatViewBindingModel model)
        {
            var user = await this.UserManager.GetUserAsync(User);
            this.groupService.CreateMessage(model.GroupId, user, model.Content);

            return RedirectToAction("Chat", "Group", new { groupId = model.GroupId, area = "Teamleader" });
        }



        public async System.Threading.Tasks.Task<IActionResult> AddUsers(int groupId)
        {
            var currentUser = await this.UserManager.GetUserAsync(User);
            var users = this.groupService.GetNotMemberUsers(groupId, currentUser);

            var usersViews = new List<UserViewModel>();

            foreach(var user in users)
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
                ExtraId = groupId,
                UserViews = usersViews
            };

            return View(viewModel);
        }


        public IActionResult AddUser(int groupId , string userId)
        {
            this.groupService.AddUserToGroup(groupId, userId);

            return RedirectToAction("AddUsers" , "Group" , new { area="Teamleader" , groupId = groupId});
        }


        public async System.Threading.Tasks.Task<IActionResult> RemoveUsers(int groupId)
        {
            var currentUser = await this.UserManager.GetUserAsync(User);
            var users = this.groupService.GetGroupMembers(groupId, currentUser);

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
                ExtraId = groupId,
                UserViews = usersViews
            };

            return View(viewModel);
        }

        public IActionResult RemoveUser(int groupId, string userId)
        {
            this.groupService.RemoveUserFromGroup(groupId, userId);

            return RedirectToAction("RemoveUsers", "Group", new { area = "Teamleader", groupId = groupId });
        }
    }
}

namespace App.Areas.Users.Controllers
{
    using DbModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Utilities;

    [Area("Users")]
    [Authorize(Roles = "User,TeamLeader")]
    public class UserController : Controller
    {
        public UserController(UserManager<User> userManager, MessageService messageService, TaskService taskService)
        {
            this.userManager = userManager;
            this.messageService = messageService;
            this.taskService = taskService;
        }

        public UserManager<User> userManager { get; set; }
        public MessageService messageService { get; set; }
        public TaskService taskService { get; set; }

    }
}

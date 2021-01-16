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
        public UserController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public UserManager<User> UserManager { get; set; }

    }
}

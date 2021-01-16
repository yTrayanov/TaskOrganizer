﻿namespace App.Areas.Teamleader.Controllers
{
    using DataContext;
    using DbModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Teamleader")]
    //[Authorize(Roles = Constants.TeamLeader)]
    public class TeamleaderController : Controller
    {
        protected TeamleaderController(UserManager<User> userManager)
        {
            this.UserManager  = userManager;
        }

        protected UserManager<User> UserManager { get; set; }
    }
}

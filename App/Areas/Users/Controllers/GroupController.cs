using DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Users.Controllers
{
    public class GroupController : UserController
    {
        public GroupController(UserManager<User> userManager, MessageService messageService, TaskService taskService) 
            : base(userManager, messageService, taskService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

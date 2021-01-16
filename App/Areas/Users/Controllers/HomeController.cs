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
    public class HomeController : UserController
    {
        public HomeController(UserManager<User> userManager, MessageService messageService, TaskService taskService) : base(userManager, messageService, taskService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGroupMessages(int id)
        {
            var messages = this.messageService.GetGroupMessages(id);

            return Ok(messages);
        }


        public async System.Threading.Tasks.Task<IActionResult> AddMessage(int groupId,string content)
        {
            var user = await this.userManager.GetUserAsync(User);

            var message = this.messageService.CreateMessage(groupId, user, content);

            return Ok(message);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> TakeIndividualTask(int taskId)
        {
            var user = await this.userManager.GetUserAsync(User);

            return Ok();
        }
    }
}

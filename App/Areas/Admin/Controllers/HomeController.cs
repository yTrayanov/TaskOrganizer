using DbModels;
using DtoModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace App.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public HomeController(UserManager<User> userManager, AdminService adminService) : base(userManager, adminService)
        {
        }

        public async Task<IActionResult> Index()
        {
            var users = this.adminService.GetAllUsers();
            var usersViewModels = new List<UserViewModel>();

            foreach(var user in users)
            {
                var roles = await this.userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel(user.Id , user.UserName , roles.Last());
                usersViewModels.Add(userViewModel);
            }


            return View(usersViewModels);
        }

        public async System.Threading.Tasks.Task<IActionResult> Promote(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            var userRoles = await this.userManager.GetRolesAsync(user);

            if (userRoles.Contains(Constants.User))
            {
                await this.userManager.AddToRoleAsync(user, Constants.TeamLeader);
            }


            return RedirectToAction("Index" , "Home" , new { area="Admin"});
        }

        public async System.Threading.Tasks.Task<IActionResult> Demote(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            var userRoles = await this.userManager.GetRolesAsync(user);

            if (userRoles.Contains(Constants.TeamLeader))
            {
                await userManager.RemoveFromRoleAsync(user, Constants.TeamLeader);
            }
            else
            {
                this.adminService.DeleteUser(user);
            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}

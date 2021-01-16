namespace App.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Utilities;
    using DbModels;
    using Services;

    [Area("Admin")]
    [Authorize(Roles = Constants.Administrator)]
    public class AdminController : Controller
    {
        protected AdminController(UserManager<User> userManager , AdminService adminService)
        {
            this.userManager = userManager;
            this.adminService = adminService;
        }

        protected UserManager<User> userManager { get; set; }
        protected AdminService adminService { get; set; }

    }
}

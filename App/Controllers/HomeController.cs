using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using DbModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Utilities;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger , UserManager<User> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {

            if (!this.User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Identity/Account/Login");
            }
            else if (this.User.IsInRole(Constants.Administrator))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home", new { area = "Users" });

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}

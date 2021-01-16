namespace App.Areas.Teamleader.Controllers
{
    using DataContext;
    using Services;

    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Identity;
    using DbModels;

    // routes: Teamleader/Task/{method}
    public class TaskController : TeamleaderController
    {
        private TaskService taskService;

        public TaskController(UserManager<User>userManager , TaskService taskService ) : base(userManager)
        {
            this.taskService = taskService;
        }

        [HttpPost]
        public ActionResult CreateTask(string content, string type, int levelOfImportance)
        {
            var newTask = this.taskService.CreateTask(content, type, levelOfImportance);

            if(newTask == null)
            {
                return BadRequest("Task data was not correct");
            }

            return Ok(newTask);
        }

        [HttpGet]
        public ActionResult GetTask(int id , string type)
        {
            var task = this.taskService.FindTaskById(id, type);

            if(task == null)
            {
                return NotFound("Task doesn't exist");
            }

            return Ok(task);
        }

        [HttpDelete]
        public ActionResult DeleteTask(int id)
        {
            bool isDeleted = this.taskService.DeleteTaskById(id);

            if (!isDeleted)
                return NotFound("Task wasn't found");

            return Ok("Task deleted successfully");
        }

        [HttpGet]
        public ActionResult IndividualTasks()
        {
            var tasks = this.taskService.GetAllIndividualTasks();

            if (tasks == null || tasks.Count == 0)
                return NotFound("There are no individual tasks");

            return Ok(tasks);
        }

        [HttpGet]
        public ActionResult GroupTasks()
        {
            var tasks = this.taskService.GetAllGroupTasks();

            if (tasks == null || tasks.Count == 0)
                return NotFound("There are no individual tasks");

            return Ok(tasks);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetUserTasks(string id = null)
        {
            User user;
            if(id != null)
            {
                user = await this.UserManager.FindByIdAsync(id);
            }
            else
            {
                user = await this.UserManager.GetUserAsync(User);
            }

            var userTasks = this.taskService.GetUserTasks(user.Id);

            return Ok(userTasks);
        }

        [HttpGet]
        public IActionResult GetGroupTasks(int id)
        {
            var tasks = this.taskService.GetGroupTasks(id);

            return Ok(tasks);
        }


    }
}

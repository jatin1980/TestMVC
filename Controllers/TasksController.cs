using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.data;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class TasksController : Controller
    {
        private readonly ILogger<TasksController> _logger;
        private readonly TestMvcDbContextDbContext _dbContext;
        //private UserManager<ApplicationUser> userManager;
        //private SignInManager<ApplicationUser> signInManager;
        public TasksController(TestMvcDbContextDbContext dbContext, ILogger<TasksController> logger
               )
        {
            _logger = logger;
            _dbContext = dbContext;
        
        }

        public IActionResult Index()
        {
            string UserId = "";
            List<Tasks> tasks = null;
            if (HttpContext.Session.GetString("UserId") !="")
            {
                UserId = HttpContext.Session.GetString("UserId");
                tasks = _dbContext.Tasks
                    .Include("User")
                    .ToList();
            
            }
          

            return View(tasks);
        }

     
        public IActionResult AddTask(string Title,string Description)
        {
          string  UserId = HttpContext.Session.GetString("UserId");
            Tasks tasks = new Tasks();
            tasks.Title = Title;
            tasks.Description = Description;
            //tasks.User = _dbContext.Users.Find(UserId);
            tasks.TaskStatus = status.NotCheckedOut;
            _dbContext.Tasks.Add(tasks);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult DeleteTask(int id)
        {
            var item = _dbContext.Tasks.Find(id);
            _dbContext.Tasks.Remove(item);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult TaskupdateDone(int id)
        {
            string UserId = HttpContext.Session.GetString("UserId");
            var item = _dbContext.Tasks.Find(id);
            item.TaskStatus = status.Done;
            item.User = _dbContext.Users.Find(UserId);
            _dbContext.Tasks.Update(item);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult TaskbacknotCheckout(int id)
        {
            var item = _dbContext.Tasks.Find(id);
            item.TaskStatus = status.NotCheckedOut;
            item.User = null;
            _dbContext.Tasks.Update(item);
            _dbContext.SaveChanges();
            return RedirectToAction("index");

        }

        public IActionResult Taskupdate(int id)
        {
            string UserId = HttpContext.Session.GetString("UserId");
            var item = _dbContext.Tasks.Find(id);
            item.TaskStatus = status.CheckedOut;
            item.User = _dbContext.Users.Find(UserId);
            _dbContext.Tasks.Update(item);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

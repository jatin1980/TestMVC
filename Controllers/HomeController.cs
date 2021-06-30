using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.data;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestMvcDbContextDbContext   _dbContext;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        public HomeController(TestMvcDbContextDbContext  dbContext,ILogger<HomeController> logger, UserManager<ApplicationUser> userMgr,
               SignInManager<ApplicationUser> signInMgr)
        {
            _logger = logger;
            _dbContext = dbContext;
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmUser(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
           
            if (user!=null)
            {
                HttpContext.Session.SetString("UserId", UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Tasks");
            }
           return RedirectToAction("Index", "Home"); 
         }
        public IActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewUser(RegisterViewModel model)
        {
            // Create  ApplicationUser account
            var user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                DisplayName = model.DisplayName,
                Email = model.Email,
                CreatedDate = DateTime.Now,

            };
            // Insert  into the Database and assign "Registered" roles to him.
            if (await userManager.FindByIdAsync(user.Id) == null)
            {
                await userManager.CreateAsync(user, "Pass4Admin");
                // await userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await userManager.AddToRoleAsync(user, "RegisteredUser");
                // Remove Lockout and E-Mail confirmation.
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
            }

            await _dbContext.SaveChangesAsync();
            HttpContext.Session.SetString("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.UserName);
            return RedirectToAction("Index", "Tasks");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

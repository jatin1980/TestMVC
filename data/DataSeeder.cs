using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.Models;
using Task = System.Threading.Tasks.Task;

namespace TestMVC.data
{
    public static class DbSeeder
    {
       
        public static void Seed(
            TestMvcDbContextDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext, roleManager, userManager)
                   .GetAwaiter()
                    .GetResult();
            }

        }
     

      
        private static async Task CreateUsers(
            TestMvcDbContextDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            //// local variables
            //DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            //DateTime lastModifiedDate = DateTime.Now;


            string role_RegisteredUser = "RegisteredUser";

            //Create Roles (if they doesn't exist yet)

            if (!await roleManager.RoleExistsAsync(role_RegisteredUser))
            {
                await roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
            }

            // Create  ApplicationUser account
            var user_jimmy = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "jimmy",
                DisplayName = "Jimmy Ahir",
                Email = "jimmy@gmail",
                CreatedDate = DateTime.Now,
               
            };
            // Insert  into the Database and assign "Registered" roles to him.
            if (await userManager.FindByIdAsync(user_jimmy.Id) == null)
            {
                await userManager.CreateAsync(user_jimmy, "Pass4Admin");
                // await userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await userManager.AddToRoleAsync(user_jimmy, role_RegisteredUser);
                // Remove Lockout and E-Mail confirmation.
                user_jimmy.EmailConfirmed = true;
                user_jimmy.LockoutEnabled = false;
            }


            // Create  registered user accounts
            var user_Ryan = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@testmakerfree.com",
                CreatedDate = DateTime.Now,
                DisplayName = "Ryan Mistri"
            };



            // Insert sample registered users into the Database and also assign the "Registered" role to him.
            if (await userManager.FindByIdAsync(user_Ryan.Id) == null)
            {
                await userManager.CreateAsync(user_Ryan, "Pass4Ryan");
                await userManager.AddToRoleAsync(user_Ryan, role_RegisteredUser);
                // Remove Lockout and E-Mail confirmation.
                user_Ryan.EmailConfirmed = true;
                user_Ryan.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();

            //if (!dbContext.Tasks.Any())
            //{
            //    // Create  registered user accounts
            //    var task1 = new Tasks()
            //    {
            //        Title = "Scrum task service bug",
            //        Description = "Fixing null reference while adding task",
            //       User = user_jimmy,
            //       TaskStatus = status.NotCheckedOut,
                    
            //    };
            //    var task2 = new Tasks()
            //    {
            //        Title = "Scrum Controller",
            //        Description = "Implementing Scrum Controller",
            //        User = user_jimmy,
            //        TaskStatus = status.NotCheckedOut,

            //    };

            //    var task3 = new Tasks()
            //    {
            //        Title = "person model",
            //        Description = "Creating personal model with data annotation",
            //        User = user_jimmy,
            //        TaskStatus = status.CheckedOut,
            //    };


            //    dbContext.Tasks.Add(task1);
            //    dbContext.Tasks.Add(task2);
            //    dbContext.Tasks.Add(task3);
            //    dbContext.SaveChanges();
            //}


        }
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC.Models;

namespace TestMVC.data
{
    public class TestMvcDbContextDbContext:IdentityDbContext<ApplicationUser>
    {
        public TestMvcDbContextDbContext(DbContextOptions<TestMvcDbContextDbContext> options) : base(options)

        {
        }
        public DbSet<Tasks>  Tasks { get; set; }

    }

}

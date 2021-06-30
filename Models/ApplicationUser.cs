using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public ApplicationUser()
        {

        }
      
        public string DisplayName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual List<Tasks> Tasks { get; set; }

    }
}

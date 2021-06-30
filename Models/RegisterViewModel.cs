using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC.Models
{
    public class RegisterViewModel
    {
        [Required]

        public string UserName { get; set; }        
        public string DisplayName { get; set; }      
        public string Email { get; set; }
    }
}

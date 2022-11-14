using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Entities.DTO
{
    public class LogInViewModel
    {

        [Required]
        public string Email { get; set; } = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public string Name { get; set; } = "";

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

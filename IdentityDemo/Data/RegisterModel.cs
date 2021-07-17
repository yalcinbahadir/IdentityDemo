using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Data
{
    public class RegisterModel : LoginModel
    {
        [Required]
        public string ConfirmPassword { get; set; }
    }
}

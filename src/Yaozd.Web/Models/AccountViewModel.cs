using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yaozd.Web.Models
{
    public class AccountViewModel
    {
        public class Login
        {
            [Required(ErrorMessage = "用户名必需填写")]
            [Display(Name = "用户名")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "密码必需填写")]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "记住我?")]
            public bool RememberMe { get; set; }
            
        }
    }
}
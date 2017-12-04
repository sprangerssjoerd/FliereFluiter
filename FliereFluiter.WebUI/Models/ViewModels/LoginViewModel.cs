using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FliereFluiter.WebUI.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public int RoleLvl { get; set; }

        public bool PasswordPass { get; set; }
        public bool UserNamePass { get; set; }
    }
}
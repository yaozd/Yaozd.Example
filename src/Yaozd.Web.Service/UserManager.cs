using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaozd.Model;

namespace Yaozd.Web.Service
{
    public class UserManager
    {
        public static User Find(string userName, string password)
        {
            return new User
            {
                Name = "Yaozd",
                Password = "pwd",
                IsDel = 1
            };
        }
    }
}

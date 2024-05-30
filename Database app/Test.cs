using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_app
{
    internal class Test
    {
        User user = new User();
        Role role = new Role();
        public Test()
        {
            user.ID = 1;
            user.Name = "John";
            user.Email = "HiuhdnioAN";
                user.PasswordHash = "password";
            user.Role_ID = 1;
            user.Role = role;
            user.Role.RoleName = "Admin";
            user.Role.Users.Add(user);
            using (var db = new DatabaseContext())
            {
                db.Users.Add(user);
                db.Roles.Add(role);
                db.SaveChanges();
            }
        }
    }
}

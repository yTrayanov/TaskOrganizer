using DataContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Services
{
    public class AdminService : Service
    {
        public AdminService(OrganizerDbContext context) : base(context)
        {
        }


        public List<User> GetAllUsers()
        {
            var users = this.Context.Users.Where(u => u.UserName != Constants.AdminUsername).ToList();

            return users;
        }

        public void DeleteUser(User user)
        {
            this.Context.Remove(user);
            this.Context.SaveChanges();
        }

    }
}

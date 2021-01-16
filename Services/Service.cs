using DataContext;
using DbModels;
using System;
using System.Linq;

namespace Services
{
    public class Service
    {
        protected Service (OrganizerDbContext context)
        {
            this.Context = context;
        }

        public OrganizerDbContext Context { get; set; }

        protected User FindUserById(string id)
        {
            return this.Context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}

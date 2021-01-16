namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using DataContext;
    using DbModels;
    using Microsoft.EntityFrameworkCore;
    using Utilities;

    public class GroupService : Service
    {
        public GroupService(OrganizerDbContext context) : base(context)
        {
        }

        public Group CreateGroup(string name , User user)
        {
            var group = new Group()
            {
                Name = name,
            };


            this.Context.Groups.Add(group);
            this.Context.SaveChanges();

            this.AddUserToGroup(group.Id, user.Id);

            return group;
        }

        public UserGroup AddUserToGroup(int groupId , string userId)
        {
            var group =this.Context.Groups.FirstOrDefault(g => g.Id == groupId);
            var user = this.Context.Users.FirstOrDefault(u => u.Id == userId);


            var userGroup = new UserGroup()
            {
                User = user,
                Group = group
            };

            this.Context.UserGroups.Add(userGroup);
            this.Context.SaveChanges();

            return userGroup;
        }

        private Group FindGroupById(int id)
        {
            return this.Context.Groups.FirstOrDefault(g => g.Id == id);
        }

        public bool CheckIfUserIsInGroup(int groupId , string userId)
        {

            bool alreadyInGroup = this.Context.UserGroups.Any(ug => ug.UserId == userId && ug.GroupId == groupId);

            return alreadyInGroup ? true : false;

        }

        public Message CreateMessage(int groupId , User user , string content)
        {
            var group = this.FindGroupById(groupId);

            var message = new Message()
            {
                Content = content,
                Sender = user,
                Group = group
            };

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();

            return message;
        }

        public List<Group> GetUserGroups(User user)
        {
            var userGroups = this.Context.UserGroups
                .Include(ug => ug.Group)
                .Where(ug => ug.User == user)
                .ToList();

            var groups = new List<Group>();

            foreach(var userGroup in userGroups)
            {
                groups.Add(this.Context.Groups.FirstOrDefault(g => g.Id == userGroup.GroupId));
            }


            return groups;
        }

        public List<Message> GetGroupMessages(int groupId)
        {
            var messages = this.Context.Messages.Where(m => m.GroupId == groupId).Include(m => m.Sender).ToList();

            return messages;
        }

        public List<User> GetNotMemberUsers(int groupId , User user)
        {
            var users = this.Context.Users
                .Where(u => 
                    !this.Context.UserGroups.Any(ug => ug.User == u && ug.GroupId == groupId) &&
                    u.UserName != Constants.AdminUsername &&
                    u != user).ToList();



            return users;
        }

        public List<User> GetGroupMembers(int groupId , User user)
        {
            var users = this.Context.Users
                .Where(u =>
                    this.Context.UserGroups.Any(ug => ug.User == u && ug.GroupId == groupId) &&
                    u.UserName != Constants.AdminUsername &&
                    u != user).ToList();

            return users;
        }

        public void RemoveUserFromGroup(int groupId, string userId)
        {

            var userGroup = this.Context.UserGroups.FirstOrDefault(ug => ug.GroupId == groupId && ug.UserId == userId);

            this.Context.UserGroups.Remove(userGroup);
            this.Context.SaveChanges();

        }
    }
}

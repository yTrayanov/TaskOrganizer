using DataContext;
using DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class MessageService : Service
    {
        public MessageService(OrganizerDbContext context) : base(context)
        {
        }

        public List<Message> GetGroupMessages(int id)
        {
            var messages = this.Context.Messages.Where(m => m.GroupId == id).ToList();

            return messages;
        }

        public Message CreateMessage(int groupId , User user ,string content)
        {
            var group = this.Context.Groups.FirstOrDefault(g => g.Id == groupId);

            var message = new Message()
            {
                Group = group,
                Sender = user,
                Content = content
            };

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();

            return message;
        }
    }
}

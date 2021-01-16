using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class MessageViewModel
    {
        public MessageViewModel(string username , string content , bool messageIsFromUser)
        {
            this.Username = username;
            this.Content = content;
            this.MessageIsFromUser = messageIsFromUser;
        }
        public string Username { get; set; }
        public string Content { get; set; }

        public bool MessageIsFromUser { get; set; }
    }
}

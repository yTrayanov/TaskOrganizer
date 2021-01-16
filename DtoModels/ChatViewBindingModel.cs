using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class ChatViewBindingModel
    {
        
        public int GroupId { get; set; }
        public string Content { get; set; }
        public List<MessageViewModel> MessageViews { get; set; }
    }
}

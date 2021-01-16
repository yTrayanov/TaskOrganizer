using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class GroupViewModel
    {
        public GroupViewModel(int id , string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

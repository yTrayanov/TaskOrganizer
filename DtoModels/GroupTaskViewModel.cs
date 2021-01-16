using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class GroupTaskViewModel
    {
        public int TaskId { get; set; }
        public List<GroupViewModel> GroupViewModels { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace DtoModels
{
    public class GroupTask : TaskAbs
    {
        

        public string GroupName { get; set; }
        public int GroupId { get; set; }

        public override bool ValidateType(string value)
        {
            if(value != Constants.GroupTaskType)
            {
                return false;
            }

            return true;
        }
    }
}

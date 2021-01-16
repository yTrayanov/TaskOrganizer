using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace DtoModels
{
    public class IndividualTask : TaskAbs
    {
       

        public string Username { get; set; }
        public string UserId { get; set; }

        public override bool ValidateType(string value)
        {
            if (value != Constants.IndividualTaskType)
            {
                return false;
            }

            return true;
        }

    }
}

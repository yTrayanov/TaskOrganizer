﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class AddRemoveUserViewBindingModel
    {
        public int ExtraId { get; set; }
        public List<UserViewModel> UserViews { get; set; }
    }
}

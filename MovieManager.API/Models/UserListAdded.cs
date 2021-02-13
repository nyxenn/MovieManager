using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class UserListAdded
    {
        public UserList UserList { get; set; }

        public bool MovieAdded { get; set; } = false;
    }
}

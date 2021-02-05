using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class ListMovie
    {
        public int? UserListID { get; set; }
        public UserList UserList { get; set; }

        public int? MovieID { get; set; }
        public DbMovie Movie { get; set; }
    }
}

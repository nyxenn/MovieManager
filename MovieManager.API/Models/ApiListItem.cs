using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class ApiListItem
    {
        public ApiListItem(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

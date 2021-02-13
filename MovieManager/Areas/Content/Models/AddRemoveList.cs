using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.Models
{
    public class AddRemoveList
    {
        public int ListID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool IsDefault { get; set; }
    }
}

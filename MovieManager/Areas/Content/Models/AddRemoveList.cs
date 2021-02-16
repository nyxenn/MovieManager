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

        public AddRemoveList()
        {
        }

        public AddRemoveList(int listID, string title, string type, bool isDefault)
        {
            ListID = listID;
            Title = title;
            Type = type;
            IsDefault = isDefault;
        }
    }
}

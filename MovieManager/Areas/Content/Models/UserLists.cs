using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.Models
{
    public class UserLists : IUserLists
    {
        private List<UserList> _defaultLists;
        private List<UserList> _customLists;

        public List<UserList> DefaultLists
        {
            get
            {
                return _defaultLists;
            }
        }

        public List<UserList> CustomLists
        {
            get
            {
                return _customLists;
            }
        }


        public UserLists()
        {
            if (_defaultLists == null)
            {
                _defaultLists = new List<UserList>();
            }

            if (_customLists == null)
            {
                _customLists = new List<UserList>();
            }
        }

        public void AddItemToList(UserList item)
        {
            if (item.IsDefault && FindItemInList(item, "default") == false)
            {
                _defaultLists.Add(item);
            }
            else if (item.IsDefault == false && FindItemInList(item) == false)
            {
                _customLists.Add(item);
            }
        }

        public void AddItemsToList(List<UserList> items)
        {
            foreach (UserList item in items)
            {
                AddItemToList(item);
            }
        }

        private bool FindItemInList(UserList item, string type = "custom")
        {

            foreach (var list in type == "default" ? DefaultLists : CustomLists)
            {
                if (list.UserListID == item.UserListID)
                {
                    return true;
                }
            }

            return false;
        }

        public List<UserList> GetLists()
        {
            return DefaultLists.Concat(CustomLists).ToList();
        }
    }
}

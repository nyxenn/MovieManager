using MMApi.Models;
using System.Collections.Generic;

namespace MovieManager.Areas.Content.Models
{
    public interface IUserLists
    {
        List<UserList> CustomLists { get; }
        List<UserList> DefaultLists { get; }

        void AddItemsToList(List<UserList> items);
        void AddItemToList(UserList item);
        List<UserList> GetLists();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMApi.Models
{
    public class UserList
    {
        [Key]
        public int UserListID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsDefault { get; set; } = false;

        public ICollection<ListMovie> Movies { get; set; }


        public UserList(string userID, string title)
        {
            UserID = userID;
            Title = title;
        }

        public UserList(string userID, string title, bool isDefault)
        {
            UserID = userID;
            Title = title;
            IsDefault = isDefault;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Commander.Models.DB
{
    public partial class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.EF.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public string RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}

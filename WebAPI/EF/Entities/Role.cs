using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.EF.Entities
{
    public class Role
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string RoleName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}

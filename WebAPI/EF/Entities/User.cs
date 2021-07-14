using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.EF.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string  Phone { get; set; }
        public string Email { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}

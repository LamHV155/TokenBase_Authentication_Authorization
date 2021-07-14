using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

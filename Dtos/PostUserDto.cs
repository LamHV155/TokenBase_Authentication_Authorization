using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos
{
    public class PostUserDto : PagedRequest
    {
        public string Keyword { get; set; }
    }
}

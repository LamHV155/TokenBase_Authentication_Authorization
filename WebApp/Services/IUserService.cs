using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Services
{
    public interface IUserService
    {
        Task<string> Login(PostLoginDto req);
        Task<bool> Register(PostUserDto req);
    }
}

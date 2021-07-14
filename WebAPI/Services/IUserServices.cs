using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IUserServices
    {
        Task<string> Register(PostRegisterDto req);
        Task<string> Authenticate(PostLoginDto req);
        Task<GetUserDto> GetUser(string UserId);
        Task<List<GetUserDto>> GetAllUser();
        Task<List<GetUserDto>> GetUserPaging(PostUserDto req);

    }
}

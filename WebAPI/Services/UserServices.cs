using Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ultilities;
using WebAPI.EF;
using WebAPI.EF.Entities;

namespace WebAPI.Services
{
    public class UserServices : IUserServices
    {
        private readonly IConfiguration _configuration;
        private readonly WebApiDbContext _context;
           
        public UserServices(IConfiguration configuration, WebApiDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> Authenticate(PostLoginDto req)
        {
            var user = await _context.Users.Where(x => x.UserName == req.UserName && x.Password.Equals(MD5.Encrypt(req.Password)))
                                        .FirstOrDefaultAsync();

            if (user == null) return null;

            var roles = await _context.UserRoles.Where(x => x.UserId == user.Id)
                                                .Select(r => r.RoleId).ToListAsync();

            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, req.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach(var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var JWToken = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Issuer"],
                    claims: userClaims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    //Using HS256 Algorithm to encrypt Token  
                    signingCredentials: creds
                ) ;
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }

        public async Task<List<GetUserDto>> GetAllUser()
        {
            var users = await _context.Users.Select(user => new GetUserDto
                                                {
                                                    Id = user.Id,
                                                    UserName = user.UserName,
                                                    Phone = user.Phone,
                                                    Email = user.Email,
                                                    LastName = user.LastName,
                                                    FirstName = user.FirstName
                                                }).ToListAsync();
            return users;
        }

        public async Task<GetUserDto> GetUser(string UserId)
        {
            var user = await _context.Users.Where(x => x.Id == new Guid(UserId))
                                    .Select(user => new GetUserDto {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Phone = user.Phone,
                                        Email = user.Email,
                                        LastName = user.LastName,
                                        FirstName = user.FirstName
                                    }).FirstOrDefaultAsync();
            return user;

        }


        public async Task<List<GetUserDto>> GetUserPaging(PostUserDto req)
        {
            var users = await _context.Users.Where(x => x.UserName.Contains(req.Keyword))
                                    .Take((req.PageIndex -1) * req.PageSize)
                                    .Skip(req.PageSize)
                                    .Select(user => new GetUserDto
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Phone = user.Phone,
                                        Email = user.Email,
                                        LastName = user.LastName,
                                        FirstName = user.FirstName
                                    }).ToListAsync();
            return users;
        }

        public async Task<string> Register(PostRegisterDto req)
        {
            var check = _context.Users.FirstOrDefault(x => x.UserName == req.UserName);
            if (check != null) return Constants.UsernameAlreadyExists;

            User user = new User
            {
                Id = Guid.NewGuid(),
                UserName = req.UserName,
                LastName = req.LastName,
                FirstName = req.FirstName,
                Phone = req.Phone,
                Email = req.Email,
                Password = MD5.Encrypt(req.Password)
            };

            try
            {
                await _context.AddAsync<User>(user);
                _context.SaveChanges();
            }
            catch
            {
                return Constants.ErrorWithEntity;
            }
            return Constants.Success;
        }
    }
}

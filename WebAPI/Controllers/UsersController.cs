using Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;
using Ultilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserServices _userService;

        public UsersController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] PostLoginDto req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var token = await _userService.Authenticate(req);

            if (string.IsNullOrEmpty(token)) return BadRequest(Constants.ErrorAuthentication);
            return Ok(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] PostRegisterDto req)
        {
            var result = await _userService.Register(req);
            if (result.Equals(Constants.Success)) return Ok();
            if (result.Equals(Constants.UsernameAlreadyExists)) return BadRequest(result);
            return StatusCode(StatusCodes.Status500InternalServerError);         
        }



        [HttpGet("GetAllUser")]
        [Authorize(Policy = Constants.RoleAdmin)]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var res = await _userService.GetAllUser();
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }

    
}

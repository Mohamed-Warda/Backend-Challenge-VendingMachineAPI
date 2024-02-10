using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Helpers;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Constants;
using VendingMachine.Domain.Entities;

namespace VendingMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        public UserController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.RegisterAsync(user);
                if (result is null || !result.IsAuthenticated)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost, Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var result = await authService.GetTokenAsync(user);

            if (result is null || !result.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpDelete, Route("Delete/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserById([FromQuery] int id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (result)
            {
                return Ok("User Deleted");
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }
    }
}

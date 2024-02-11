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
        private readonly ILogger<UserController> _logger;


        public UserController(IAuthService authService, IUserService userService, ILogger<UserController> logger)
        {
            this.authService = authService;
            this.userService = userService;
            _logger = logger;
        }
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            try
            {
                _logger.LogInformation($"Invoking 'Register' EndPoint in 'UserController'");
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
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'Register' EndPoint in 'UserController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }

        }
        [HttpPost, Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            try
            {
                _logger.LogInformation($"Invoking 'Login' EndPoint in 'UserController'");
                var result = await authService.GetTokenAsync(user);

                if (result is null || !result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'Login' EndPoint in 'UserController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }


        [HttpDelete, Route("Delete/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserById([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation($"Invoking 'DeleteUserById' EndPoint in 'UserController'");
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
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'DeleteUserById' EndPoint in 'UserController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }

        }


        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                _logger.LogInformation($"Invoking 'GetAllUser' EndPoint in 'UserController'");
                var users = await userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'GetAllUser' EndPoint in 'UserController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }

        [HttpPut, Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                _logger.LogInformation($"Invoking 'ChangePassword' EndPoint in 'UserController'");
                if (ModelState.IsValid)
                {

                    var result = await userService.ChangePasswordAsync(dto);
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest();

                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'ChangePassword' EndPoint in 'UserController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }
    }
}

using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.API.Controllers;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Helpers;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly ILogger<UserController> _logger;

        public UserControllerTests()
        {
            this.authService = A.Fake<IAuthService>();
            this.userService = A.Fake<IUserService>();
            _logger = A.Fake<ILogger<UserController>>();
        }
        [Fact]
        public async void UserController_Register_ReturnOkObjectResult()
        {
            //arrange
            var registerDto = A.Fake<RegisterDto>();
            var authModel = A.Fake<AuthModel>();
            authModel.IsAuthenticated = true;
            A.CallTo(() => authService.RegisterAsync(registerDto)).Returns(authModel);

            var controller = new UserController(authService, userService, _logger);

            //act
            var result = await controller.Register(registerDto);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_Login_ReturnOkObjectResult()
        {
            //arrange
            var loginDto = A.Fake<LoginDto>();
            var authModel = A.Fake<AuthModel>();
            authModel.IsAuthenticated = true;
            A.CallTo(() => authService.GetTokenAsync(loginDto)).Returns(authModel);

            var controller = new UserController(authService, userService, _logger);

            //act
            var result = await controller.Login(loginDto);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_DeleteUserById_ReturnOkObjectResult()
        {
            //arrange
            var id = 1;
            var authModel = A.Fake<AuthModel>();
            A.CallTo(() => userService.DeleteUserAsync(id)).Returns(true);

            var controller = new UserController(authService, userService, _logger);

            //act
            var result = await controller.DeleteUserById(id);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_GetAllUser_ReturnOkObjectResult()
        {
            //arrange
            var users = A.Fake<IEnumerable<User>>();
            A.CallTo(() => userService.GetAllUsers()).Returns(users);

            var controller = new UserController(authService, userService, _logger);

            //act
            var result = await controller.GetAllUser();
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_ChangePassword_ReturnOkObjectResult()
        {
            //arrange
            var dto = A.Fake<ChangePasswordDto>();
            A.CallTo(() => userService.ChangePasswordAsync(dto)).Returns(true);

            var controller = new UserController(authService, userService, _logger);

            //act
            var result = await controller.ChangePassword(dto);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }
    }
}

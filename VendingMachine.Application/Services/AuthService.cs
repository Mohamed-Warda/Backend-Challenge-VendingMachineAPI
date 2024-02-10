using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Helpers;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<User> userManager, IOptions<JWT> jwt)
        {
            this._userManager = userManager;
            _jwt = jwt.Value;
        }


        public async Task<AuthModel> RegisterAsync(RegisterDto userDto)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(userDto.Email);
                if (user is not null)
                {
                    return new AuthModel { Message = "Email Already Exists" };
                }

                var userToAdd = new User
                {
                    UserName = new MailAddress(userDto.Email).User,
                    Email = userDto.Email,
                };

                var result = await _userManager.CreateAsync(userToAdd, userDto.Password);
                if (!result.Succeeded)
                {
                    var errorsMsg = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errorsMsg += $"{error.Description}, ";
                    }
                    return new AuthModel { Message = errorsMsg };

                }
                await _userManager.AddToRoleAsync(userToAdd, userDto.Role);

                var jwtSecurityToken = await CreateJwtToken(userToAdd);
                var roles = new List<string>();
                roles.Add(userDto.Role);
                return new AuthModel
                {
                    Email = userToAdd.Email,
                    Username = userToAdd.UserName,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = roles,
                    Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)}",
                };
            }
            catch (Exception ex)
            {
                return new AuthModel
                {
                    IsAuthenticated = false,
                };

            }
        }
        public async Task<AuthModel> GetTokenAsync(LoginDto model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)}";
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }
        public async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim("roles", role));
            }
            userClaims.Add(new Claim("UserId", user.Id.ToString()));

            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Helpers;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto userDto);
        Task<JwtSecurityToken> CreateJwtToken(User user);
        Task<AuthModel> GetTokenAsync(LoginDto model);
    }
}

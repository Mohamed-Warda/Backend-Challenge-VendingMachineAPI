using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.IServices;

namespace VendingMachine.Application.Services
{

    public class TokenClaimsService : ITokenClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return _httpContextAccessor.HttpContext?.User.Claims;
        }
        public string? GetLoggedInUserId()
        {
            var userClaims = GetUserClaims();
            return userClaims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
    }
}

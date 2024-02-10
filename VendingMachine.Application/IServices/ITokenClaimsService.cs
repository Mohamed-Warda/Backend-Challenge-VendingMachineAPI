using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.IServices
{
    public interface ITokenClaimsService
    {
        IEnumerable<Claim> GetUserClaims();
        string? GetLoggedInUserId();
    }
}

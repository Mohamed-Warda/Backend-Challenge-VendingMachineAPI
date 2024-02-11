using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenClaimsService tokenClaims;

        public UserService(UserManager<User> userManager, ITokenClaimsService tokenClaims)
        {
            this._userManager = userManager;
            this.tokenClaims = tokenClaims;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;

        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var userId = tokenClaims.GetLoggedInUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            return false;

        }


    }
}

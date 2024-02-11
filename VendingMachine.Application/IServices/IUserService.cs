using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.IServices
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
    }
}

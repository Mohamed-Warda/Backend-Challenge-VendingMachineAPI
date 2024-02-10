using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Interfaces;

namespace VendingMachine.Application.IServices
{
    public interface ITransactionService
    {

        Task<PurchasingDto> BuyProduct(PurchasedProductDto dto);
        Task<bool> ResetAsync();
        Task<bool> DepositeAsync(DepositeMoneyDto dto);
        Task<PurchasingDto> ConfirmPurchaseAsync();
    }

}

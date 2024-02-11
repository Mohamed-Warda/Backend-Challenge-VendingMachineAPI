using Microsoft.AspNetCore.Identity;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Interfaces;

namespace VendingMachine.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly UserManager<User> _userManager;
    private readonly IBaseRepository<Transaction> transactionRepository;
    private readonly IBaseRepository<Product> productRepository;
    private readonly ITokenClaimsService tokenClaims;


    public TransactionService(UserManager<User> userManager, IBaseRepository<Transaction> transactionRepository, ITokenClaimsService tokenClaims, IBaseRepository<Product> productRepository)
    {
        this._userManager = userManager;
        this.transactionRepository = transactionRepository;
        this.tokenClaims = tokenClaims;
        this.productRepository = productRepository;
    }
    public async Task<PurchasingDto> BuyProduct(PurchasedProductDto dto)
    {
        var buyerId = tokenClaims.GetLoggedInUserId();
        var buyer = await _userManager.FindByIdAsync(buyerId);
        var id = int.Parse(buyerId);
        var product = await productRepository.GetByIdAsync(dto.ProductId);
        if (dto.Amount > product.AmountAvailable)
        {
            return new PurchasingDto()
            {
                Message = $"Only {product.AmountAvailable} {product.ProductName} Available"
            };
        }
        var totalCost = product.Cost * dto.Amount;
        var tranactionHistory = await transactionRepository.GetAllWithExpressonAsync(e => !e.IsConfirmed && e.UserId == id);

        var remainingDeposit = buyer.Deposit - tranactionHistory.Sum(x => x.TotalPaid);
        if (remainingDeposit < totalCost)
        {
            return new PurchasingDto()
            {
                Message = $"Please Deposite More Money You Only have {buyer.Deposit} and the Products Cost {totalCost}"
            };
        }

        var tranaction = new Transaction()
        {
            ProductId = dto.ProductId,
            AmountOfProducts = dto.Amount,
            UserId = int.Parse(buyerId),
            TotalPaid = totalCost,
            IsConfirmed = false,
            ProductName = product.ProductName
        };
        await transactionRepository.AddAsync(tranaction);


        var change = CalculateChange(remainingDeposit - totalCost);
        return new PurchasingDto { MoneyChange = change, Message = "Done" };
    }


    public async Task<bool> ResetAsync()
    {
        var buyerId = int.Parse(tokenClaims.GetLoggedInUserId());
        var tranactionHistory = await transactionRepository.GetAllWithExpressonAsync(e => !e.IsConfirmed && e.UserId == buyerId);
        var rowsAffected = await transactionRepository.DeleteRange(tranactionHistory);
        if (rowsAffected > 0)
        {
            return true;
        }
        return false;

    }

    public async Task<bool> DepositeAsync(DepositeMoneyDto dto)
    {
        var buyerId = tokenClaims.GetLoggedInUserId();
        var user = await _userManager.FindByIdAsync(buyerId);
        if (user.Deposit is null)
        {

            user.Deposit = dto.DepositeMoney;
        }
        else
        {
            user.Deposit += dto.DepositeMoney;

        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public async Task<PurchasingDto> ConfirmPurchaseAsync()
    {
        var products = new Dictionary<string, int>();
        var buyerId = int.Parse(tokenClaims.GetLoggedInUserId());
        using (var tran = await transactionRepository.BeginTransaction())
        {

            var tranactions = await transactionRepository.GetAllWithExpressonAsync(e => !e.IsConfirmed && e.UserId == buyerId);
            var totalCost = tranactions.Sum(x => x.TotalPaid);
            foreach (var tranaction in tranactions)
            {
                tranaction.IsConfirmed = true;
                products.Add(tranaction.ProductName, tranaction.AmountOfProducts);

                var product = await productRepository.GetByIdAsync(tranaction.ProductId);
                product.AmountAvailable -= tranaction.AmountOfProducts;
                await productRepository.UpdateAsync(product);
            }
            await transactionRepository.UpdateRangeAsync(tranactions);

            var buyer = await _userManager.FindByIdAsync(buyerId.ToString());
            var change = CalculateChange(buyer.Deposit - totalCost);

            if (buyer != null)
            {
                buyer.Deposit = 0;
            }
            await _userManager.UpdateAsync(buyer);

            await transactionRepository.CommitTranaction(tran);
            return new PurchasingDto { MoneyChange = change, Message = "Successfully Purchased. Please wait for the products.", Products = products };

        }

    }
    private Dictionary<int, int> CalculateChange(int? totalCost)
    {
        if (totalCost.HasValue)
        {
            var change = new Dictionary<int, int>
        {
            { 100, totalCost.Value / 100 },
            { 50, (totalCost.Value % 100) / 50 },
            { 20, ((totalCost.Value % 100) % 50) / 20 },
            { 10, (((totalCost.Value % 100) % 50) % 20) / 10 },
            { 5, ((((totalCost.Value % 100) % 50) % 20) % 10) / 5 }
        };

            return change;
        }

        return null;
    }




}

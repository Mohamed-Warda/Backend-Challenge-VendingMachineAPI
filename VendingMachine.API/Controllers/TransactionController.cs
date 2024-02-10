using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Constants;

namespace VendingMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            this.transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost, Route("BuyProduct")]
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> BuyProduct(PurchasedProductDto dto)
        {
            try
            {
                _logger.LogInformation($"Invoking 'BuyProduct' EndPoint in 'TransactionController'");

                if (ModelState.IsValid)
                {
                    var result = await transactionService.BuyProduct(dto);

                    return Ok(result);
                }
                return BadRequest(ModelState);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'BuyProduct' EndPoint in 'TransactionController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }


        [HttpPost, Route("ResetPurchases")]
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> ResetPurchases()
        {
            try
            {
                _logger.LogInformation($"Invoking 'ResetPurchases' EndPoint in 'TransactionController'");

                var result = await transactionService.ResetAsync();
                if (result)
                {
                    return Ok("Done");

                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'ResetPurchases' EndPoint in 'TransactionController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }


        [HttpPost, Route("DepositeMoney")]
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> DepositeMoney(DepositeMoneyDto dto)
        {
            try
            {
                _logger.LogInformation($"Invoking 'DepositeMoney' EndPoint in 'TransactionController'");
                if (ModelState.IsValid)
                {

                    var result = await transactionService.DepositeAsync(dto);
                    if (result)
                    {
                        return Ok("Done");

                    }
                    return BadRequest();
                }
                return BadRequest();

            }

            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'DepositeMoney' EndPoint in 'TransactionController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }



        [HttpPost, Route("ConfirmPurchase")]
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> ConfirmPurchase()
        {
            try
            {
                _logger.LogInformation($"Invoking 'ConfirmPurchase' EndPoint in 'TransactionController'");

                var result = await transactionService.ConfirmPurchaseAsync();
                return Ok(result);


            }

            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'ConfirmPurchase' EndPoint in 'TransactionController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }
    }

}

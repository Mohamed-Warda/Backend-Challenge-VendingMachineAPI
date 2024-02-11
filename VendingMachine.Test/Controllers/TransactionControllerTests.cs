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
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Test.Controllers
{
    public class TransactionControllerTests
    {

        private readonly ITransactionService transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionControllerTests()
        {
            transactionService = A.Fake<ITransactionService>();
            _logger = A.Fake<ILogger<TransactionController>>();
        }
        [Fact]
        public async void TransactionController_BuyProduct_ReturnOkObjectResult()
        {
            //arrange
            var purchasedProductDto = A.Fake<PurchasedProductDto>();
            var purchasingDto = A.Fake<PurchasingDto>();
            A.CallTo(() => transactionService.BuyProduct(purchasedProductDto)).Returns(purchasingDto);

            var controller = new TransactionController(transactionService, _logger);

            //act
            var result = await controller.BuyProduct(purchasedProductDto);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void TransactionController_ResetPurchases_ReturnOkObjectResult()
        {
            //arrange
            var purchasedProductDto = A.Fake<PurchasedProductDto>();
            var purchasingDto = A.Fake<PurchasingDto>();
            A.CallTo(() => transactionService.ResetAsync()).Returns(true);

            var controller = new TransactionController(transactionService, _logger);

            //act
            var result = await controller.ResetPurchases();
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public async void TransactionController_DepositeMoney_ReturnOkObjectResult()
        {
            //arrange
            var depositeMoneyDto = A.Fake<DepositeMoneyDto>();
            var purchasingDto = A.Fake<PurchasingDto>();
            A.CallTo(() => transactionService.DepositeAsync(depositeMoneyDto)).Returns(true);

            var controller = new TransactionController(transactionService, _logger);

            //act
            var result = await controller.DepositeMoney(depositeMoneyDto);
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void TransactionController_ConfirmPurchase_ReturnOkObjectResult()
        {
            //arrange
            var purchasingDto = A.Fake<PurchasingDto>();
            A.CallTo(() => transactionService.ConfirmPurchaseAsync()).Returns(purchasingDto);

            var controller = new TransactionController(transactionService, _logger);

            //act
            var result = await controller.ConfirmPurchase();
            //assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}

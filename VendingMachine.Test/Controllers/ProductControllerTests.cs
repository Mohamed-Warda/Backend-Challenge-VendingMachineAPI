using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VendingMachine.API.Controllers;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Test.Controllers;

public class ProductControllerTests
{
    private readonly IProductService productService;
    private readonly ILogger<ProductController> _logger;

    public ProductControllerTests()
    {
        productService = A.Fake<IProductService>();
        _logger = A.Fake<ILogger<ProductController>>();
    }
    [Fact]
    public async void ProductController_AddProduct_ReturnOk()
    {
        //arrange
        var productDto = A.Fake<ProductDto>();
        var product = A.Fake<Product>();
        A.CallTo(() => productService.CreateProduct(productDto)).Returns(product);

        var controller = new ProductController(productService, _logger);

        //act
        var result = await controller.AddProduct(productDto);
        //assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
    [Fact]
    public async void ProductController_GetAllProducts_ReturnOk()
    {
        //arrange
        var products = A.Fake<IEnumerable<Product>>();
        var controller = new ProductController(productService, _logger);
        A.CallTo(() => productService.GetAllProducts()).Returns(products);

        //act
        var result = await controller.GetAllProducts();
        //assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public async void ProductController_DeleteProductById_ReturnOk()
    {
        //arrange
        var id = 1;
        var boolResult = true;
        var controller = new ProductController(productService, _logger);
        A.CallTo(() => productService.DeleteProductById(id)).Returns(boolResult);

        //act
        var result = await controller.DeleteProductById(id);
        //assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public async void ProductController_UpdateProduct_ReturnOk()
    {
        //arrange
        var productDto = A.Fake<UpdateProductDto>();
        var product = A.Fake<Product>();
        var controller = new ProductController(productService, _logger);
        A.CallTo(() => productService.UpdateProduct(productDto)).Returns(product);

        //act
        var result = await controller.UpdateProduct(productDto);
        //assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
}



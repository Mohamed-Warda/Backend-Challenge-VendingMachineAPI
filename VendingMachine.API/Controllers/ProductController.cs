using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Constants;
using VendingMachine.Domain.Entities;

namespace VendingMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            this.productService = productService;
            _logger = logger;
        }
        [HttpPost, Route("AddProduct")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            try
            {
                _logger.LogInformation($"Invoking 'AddProduct' EndPoint in 'ProductController'");

                if (ModelState.IsValid)
                {
                    var product = await productService.CreateProduct(dto);
                    return Ok(product);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'AddProduct' EndPoint in 'ProductController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");

            }
        }

        [HttpPost, Route("GetAllProducts")]
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogInformation($"Invoking 'GetAllProducts' EndPoint in 'ProductController'");
                var products = await productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'GetAllProducts' EndPoint in 'ProductController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");
            }


        }

        [HttpDelete, Route("DeleteProductById")]
        [Authorize(Roles = Roles.Seller)]

        public async Task<IActionResult> DeleteProductById(int id)
        {
            try
            {
                _logger.LogInformation($"Invoking 'DeleteProductById' EndPoint in 'ProductController'");

                var result = await productService.DeleteProductById(id);

                if (result)
                {
                    return Ok();
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'DeleteProductById' EndPoint in 'ProductController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut, Route("UpdateProduct")]
        [Authorize(Roles = Roles.Seller)]

        public async Task<IActionResult> UpdateProduct(UpdateProductDto product)
        {
            try
            {
                _logger.LogInformation($"Invoking 'UpdateProduct' EndPoint in 'ProductController'");
                if (ModelState.IsValid)
                {
                    var result = await productService.UpdateProduct(product);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Error When Invoking 'UpdateProduct' EndPoint in 'ProductController' : {ex.Message} ");
                return StatusCode(500, "Internal Server Error");
            }
        }



    }
}

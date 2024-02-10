using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.IServices;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Interfaces;
using VendingMachine.Infrastructure.Repositories;

namespace VendingMachine.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly ITokenClaimsService tokenClaims;

        public ProductService(IBaseRepository<Product> productRepository, ITokenClaimsService tokenClaims)
        {
            this.productRepository = productRepository;
            this.tokenClaims = tokenClaims;
        }
        public async Task<Product> CreateProduct(ProductDto product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                AmountAvailable = product.AmountAvailable,
                Cost = product.Cost,
                SellerId = int.Parse(tokenClaims.GetLoggedInUserId()),
            };

            return await productRepository.AddAsync(newProduct);

        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await productRepository.GetAllAsync();
        }
        public async Task<bool> DeleteProductById(int id)
        {
            var sellerId = tokenClaims.GetLoggedInUserId();
            var product = await productRepository.GetByIdAsync(id);
            //The seller who created the product is the only one allowed to delete it.
            if (product != null && sellerId != null && sellerId == product.SellerId.ToString())
            {

                var rowsEffected = await productRepository.Delete(product);
                if (rowsEffected > 0)
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<Product> UpdateProduct(UpdateProductDto product)
        {
            var sellerId = tokenClaims.GetLoggedInUserId();
            var productToUpade = await productRepository.GetByIdAsync(product.Id);
            //The seller who created the product is the only one allowed to update it.
            if (product != null && sellerId != null && sellerId == productToUpade.SellerId.ToString())
            {
                productToUpade.ProductName = product.ProductName;
                productToUpade.Cost = product.Cost;
                productToUpade.AmountAvailable = product.AmountAvailable;
                var updatedProduct = await productRepository.UpdateAsync(productToUpade);
                return updatedProduct;

            }
            return new Product();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Dtos;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.IServices
{
    public interface IProductService
    {
        Task<Product> CreateProduct(ProductDto product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<bool> DeleteProductById(int id);
        Task<Product> UpdateProduct(UpdateProductDto product);
    }
}

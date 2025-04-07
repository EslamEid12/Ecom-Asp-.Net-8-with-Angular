using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dto;
using Ecom.Core.Entitiy.Product;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenaricRepository<Product>
    {
        Task<bool>AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(Product product);

    }
}

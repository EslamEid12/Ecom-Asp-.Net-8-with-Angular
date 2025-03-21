using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entitiy.Product;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;

namespace Ecom.Inferastructure.Repository
{
    public class ProductRepository : GenaricRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

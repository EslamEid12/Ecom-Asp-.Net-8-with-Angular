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
    public class CategoryRepository : GenaricRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

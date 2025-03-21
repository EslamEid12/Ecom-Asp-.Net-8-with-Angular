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
    public class PhotoRepository : GenaricRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

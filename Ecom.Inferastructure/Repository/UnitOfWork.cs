using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;

namespace Ecom.Inferastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IProductRepository ProductRepository { get; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            CategoryRepository=new CategoryRepository(_appDbContext);
            PhotoRepository = new PhotoRepository(_appDbContext);
            ProductRepository = new ProductRepository(_appDbContext);

        }
    }
}
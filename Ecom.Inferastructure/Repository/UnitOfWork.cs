using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Inferastructure.Data;

namespace Ecom.Inferastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IProductRepository ProductRepository { get; }
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public UnitOfWork(AppDbContext appDbContext, IMapper mapper, IImageManagementService imageManagementService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            CategoryRepository = new CategoryRepository(_appDbContext);
            PhotoRepository = new PhotoRepository(_appDbContext);
            ProductRepository = new ProductRepository(_appDbContext,_mapper,_imageManagementService);
            
        }
    }
}
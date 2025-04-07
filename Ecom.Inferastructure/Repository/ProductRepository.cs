using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entitiy.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Inferastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecom.Inferastructure.Repository
{
    public class ProductRepository : GenaricRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext appDbContext, IMapper mapper, IImageManagementService imageManagementService) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null) return false;
            var product = _mapper.Map<Product>(productDto);
            await _appDbContext.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            var imagePaht = await _imageManagementService.AddImageAsync(productDto.Photo, productDto.Name);
            var photo = imagePaht.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.ID,
            }).ToList();
            await _appDbContext.Photos.AddRangeAsync(photo);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

      

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null)
            {
                return false;
            }
            var FindProduct = await _appDbContext.Products.Include(m => m.Category)
                .Include(m => m.photos)
                .FirstOrDefaultAsync(m => m.ID == updateProductDto.Id);
            if (FindProduct is null)
            {
                return false;
            }
            _mapper.Map(updateProductDto, FindProduct);
            var FindPhoto = await _appDbContext.Photos.Where(m => m.ProductId == updateProductDto.Id).ToListAsync();
            foreach (var item in FindPhoto)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _appDbContext.Photos.RemoveRange(FindPhoto);
            var ImagePath = await _imageManagementService.AddImageAsync(updateProductDto.Photo, updateProductDto.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDto.Id,
            }).ToList();
            await _appDbContext.Photos.AddRangeAsync(photo);
            await _appDbContext.SaveChangesAsync();
            return true;
            {
            }
        }
        public async Task DeleteAsync(Product product)
        {
            var photo=await _appDbContext.Photos.Where(m=>m.ProductId==product.ID).ToListAsync();
            foreach (var item in photo) 
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync(); 
        }
    }
}
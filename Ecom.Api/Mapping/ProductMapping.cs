﻿using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entitiy.Product;

namespace Ecom.Api.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ForMember(x=>x.CategoryName,op=>op.MapFrom(src=>src.Category.Name)).ReverseMap();
            CreateMap<Photo,PhotoDto>().ReverseMap();
            CreateMap<AddProductDto, Product>().ForMember(x => x.photos, op => op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDto, Product>().ForMember(x => x.photos, op => op.Ignore()).ReverseMap();
        }

    }
}

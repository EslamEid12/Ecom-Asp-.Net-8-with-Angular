using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entitiy.Product;

namespace Ecom.Api.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        }

    }
}

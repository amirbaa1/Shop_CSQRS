using AutoMapper;
using Product.Application.Features.Category.Commands.Create;
using Product.Application.Features.Category.Queries.GetCategory;
using Product.Domain.Model;
using Product.Domain.Model.Dto;

namespace Product.Application.Mapping;

public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        CreateMap<Category, GetCategoryQuery>().ReverseMap();
        CreateMap<CategoryDto, GetCategoryQuery>().ReverseMap();

        CreateMap<CategoryDto, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        
    }
}
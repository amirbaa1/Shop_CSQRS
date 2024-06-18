using AutoMapper;
using Product.Application.Features.Queries.GetProductList;
using Product.Domain.Model;

namespace Product.Application.Mapping;

public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        CreateMap<Category, GetProductListQuery>().ReverseMap();
        CreateMap<Category, CategoryResponse>().ReverseMap();
    }
}
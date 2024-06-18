using AutoMapper;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.Update;
using Product.Application.Features.Queries.GetProductList;
using Product.Domain.Model.Dto;

namespace Product.Application.Mapping;

public class ProductMapProfile : Profile
{
    public ProductMapProfile()
    {
        CreateMap<Domain.Model.Product, ProductResponse>().ReverseMap();
        CreateMap<List<Domain.Model.Product>, ProductResponse>().ReverseMap();

        CreateMap<ProductDto, CreateProductCommand>().ReverseMap();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ReverseMap();
    }
}
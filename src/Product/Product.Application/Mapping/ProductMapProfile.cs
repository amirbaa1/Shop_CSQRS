using AutoMapper;
using Product.Application.Features.Category.Commands;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.Update;
using Product.Application.Features.Queries.GetProductList;
using Product.Domain.Model;
using Product.Domain.Model.Dto;

namespace Product.Application.Mapping;

public class ProductMapProfile : Profile
{
    public ProductMapProfile()
    {
        CreateMap<Domain.Model.Product, CreateProductCommand>().ReverseMap();
        CreateMap<ProductDto, CreateProductCommand>().ReverseMap();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ReverseMap();
    }
}
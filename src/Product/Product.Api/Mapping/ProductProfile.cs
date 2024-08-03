using AutoMapper;
using Contracts.Product;
using Contracts.Store;
using Product.Api.Features.Product.Commands.CreateProduct;
using Product.Api.Features.Product.Commands.Update.UpdateProduct;
using Product.Api.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;
using Store.Domain.Model.Dto;

namespace Product.Api.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Domain.Model.Product, CreateProductCommand>().ReverseMap();
        CreateMap<ProductDto, CreateProductCommand>().ReverseMap();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ReverseMap();
        
        CreateMap<ProductStatusCommand, UpdateProductStatusDto>().ReverseMap();

        CreateMap<UpdateProductRequest, UpdateProductNameDto>().ReverseMap();

        CreateMap<UpdateStoreStatusRequest, UpdateProductStatusDto>().ReverseMap();
    }
}
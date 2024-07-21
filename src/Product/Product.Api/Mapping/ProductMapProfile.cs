using AutoMapper;
using EventBus.Messages.Event.Product;
using Product.Api.Features.Product.Commands.CreateProduct;
using Product.Api.Features.Product.Commands.Update.UpdateProduct;
using Product.Api.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;

namespace Product.Api.Mapping;

public class ProductMapProfile : Profile
{
    public ProductMapProfile()
    {
        CreateMap<Domain.Model.Product, CreateProductCommand>().ReverseMap();
        CreateMap<ProductDto, CreateProductCommand>().ReverseMap();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ReverseMap();

        CreateMap<UpdateProductCommand, ProductQueueEvent>().ReverseMap();

        CreateMap<UpdateProductStatusEvent, UpdateProductStatusDto>().ReverseMap();
        CreateMap<ProductStatusCommand, UpdateProductStatusDto>().ReverseMap();
    }
}
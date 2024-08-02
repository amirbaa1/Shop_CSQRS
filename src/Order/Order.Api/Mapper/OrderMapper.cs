using AutoMapper;
using Order.Api.Features.Order.Commands.Create;
using Order.Domain.Model;
using Order.Domain.Model.Dto;

namespace Order.Api.Mapper;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<OrderModelDto, OrderModel>().ReverseMap();
        CreateMap<OrderLineDto, OrderLine>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<AddOrderDto, OrderModel>().ReverseMap();
        CreateMap<AddOrderLineDto, OrderLine>().ReverseMap();
        CreateMap<CreateOrderCommand, OrderModelDto>().ReverseMap();
        CreateMap<AddOrderLineDto, OrderLineDto>().ReverseMap();
    }
}
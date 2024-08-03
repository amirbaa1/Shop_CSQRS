using AutoMapper;
using Basket.Api.Features.Basket.Commands.CheckOut;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Contracts.Basket;

//using EventBus.Messages.Event.Basket;
//using EventBus.Messages.Event.Store;


namespace Basket.Api.Mapper
{
    public class BasketMap : Profile
    {
        public BasketMap()
        {
            CreateMap<BasketModel, BasketModelDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<BasketModel, BasketItem>().ReverseMap();
            CreateMap<BasketModelDto, BasketItemDto>().ReverseMap();
            CreateMap<BasketItem, BasketModelDto>().ReverseMap();
            CreateMap<AddItemToBasketDto, BasketItem>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, BasketModelDto>().ReverseMap();
            CreateMap<AddItemToBasketDto, ProductDto>().ReverseMap();
        }
    }
}
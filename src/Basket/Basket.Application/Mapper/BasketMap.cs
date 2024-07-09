using AutoMapper;
using Basket.Application.Features.Basket.Commands.CheckOut;
using Basket.Application.Features.Basket.Commands.Create.Message;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Store;


namespace Basket.Application.Mapper
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

            CreateMap<CheckOutCommand, BasketQueueEvent>().ReverseMap();

            CreateMap<MessageResultCommand, MessageCheckStoreEvent>().ReverseMap();
        }
    }
}
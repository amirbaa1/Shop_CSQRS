using AutoMapper;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;


namespace Basket.Application.Mapper
{
    public class BasketMap : Profile
    {
        public BasketMap()
        { 
            CreateMap<BasketModel,BasketModelDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<BasketModel,BasketItem>().ReverseMap();
            CreateMap<BasketModelDto, BasketItemDto>().ReverseMap();
            CreateMap<BasketItem, BasketModelDto>().ReverseMap();
            CreateMap<AddItemToBasketDto, BasketItem>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, BasketModelDto>().ReverseMap();
            CreateMap<AddItemToBasketDto, ProductDto>().ReverseMap();

            //CreateMap<CheckOutBasketDto, BasketCheckoutMessage>().ReverseMap();


        }
    }
}

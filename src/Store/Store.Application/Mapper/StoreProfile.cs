using AutoMapper;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Product;
using Store.Application.Feature.Store.Commands.Create;
using Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Domain.Model.Dto;


namespace Store.Application.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<UpdateNumberDto, UpdateStoreNumberCommand>().ReverseMap();

            CreateMap<ProductStoreEvent, CreateStoreCommand>().ReverseMap();

            CreateMap<BasketStoreEvent, UpdateStoreNumberCommand>().ReverseMap();
        }
    }
}
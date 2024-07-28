using AutoMapper;
using Contracts.Product;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Store;
using Store.Api.Feature.Store.Commands.Create;
using Store.Api.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Api.Feature.Store.Queries.Check;
using Store.Domain.Model.Dto;


namespace Store.Worker.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<UpdateNumberDto, UpdateStoreNumberCommand>().ReverseMap();

            CreateMap<ProductAddStoreRequest, CreateStoreCommand>().ReverseMap();

            CreateMap<BasketStoreEvent, UpdateStoreNumberCommand>().ReverseMap();

            CreateMap<CheckStoreEvent, CheckStoreQuery>().ReverseMap();

            CreateMap<ProductAddStoreRequest,StoreDto>().ReverseMap();
        }
    }
}
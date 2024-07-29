using AutoMapper;
using Contracts.Basket;
using Contracts.Product;
using Contracts.Store;
using EventBus.Messages.Event.Basket;
using Product.Domain.Model.Dto;
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

            // CreateMap<BasketStoreEvent, UpdateStoreNumberCommand>().ReverseMap();

            // CreateMap<CheckStoreEvent, CheckStoreQuery>().ReverseMap();

            // CreateMap<CheckNumberDto,>()

            CreateMap<ProductAddStoreRequest, StoreDto>().ReverseMap();

            CreateMap<UpdateProductRequest, UpdateProductNameDto>().ReverseMap();

            CreateMap<UpdateStoreStatusRequest, UpdateProductStatusDto>().ReverseMap();

            CreateMap<CheckOutStoreRequest, CheckNumberDto>().ReverseMap();
        }
    }
}
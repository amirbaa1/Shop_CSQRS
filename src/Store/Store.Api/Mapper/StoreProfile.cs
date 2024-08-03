using AutoMapper;
using Contracts.Product;
using Store.Api.Feature.Store.Commands.Create;
using Store.Api.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Domain.Model.Dto;


namespace Store.Api.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<UpdateNumberDto, UpdateStoreNumberCommand>().ReverseMap();

            CreateMap<ProductAddStoreRequest, CreateStoreCommand>().ReverseMap();
        }
    }
}
using AutoMapper;
using Contracts.Basket;
using Contracts.Product;
using Store.Domain.Model.Dto;


namespace Store.Worker.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {

            CreateMap<ProductAddStoreRequest, StoreDto>().ReverseMap();

            CreateMap<UpdateProductRequest, UpdateProductNameDto>().ReverseMap();
            
            CreateMap<CheckOutStoreRequest, CheckNumberDto>().ReverseMap();

            CreateMap<CheckOutStoreRequest, UpdateNumberDto>().ReverseMap();
        }
    }
}
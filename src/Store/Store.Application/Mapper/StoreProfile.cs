using AutoMapper;
using Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Domain.Model.Dto;


namespace Store.Application.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<UpdateNumberDto,UpdateStoreNumberCommand>().ReverseMap();
        }
    }
}

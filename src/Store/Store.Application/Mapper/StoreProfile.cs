﻿using AutoMapper;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Product;
using EventBus.Messages.Event.Store;
using Store.Application.Feature.Store.Commands.Create;
using Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Application.Feature.Store.Queries.Check;
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

            CreateMap<CheckStoreEvent, CheckStoreQuery>().ReverseMap();
        }
    }
}
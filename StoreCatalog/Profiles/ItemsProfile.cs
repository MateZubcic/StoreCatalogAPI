using System;
using AutoMapper;
using StoreCatalog.Dtos;
using StoreCatalog.Entities;

namespace StoreCatalog.Profiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();
        }
    }
}
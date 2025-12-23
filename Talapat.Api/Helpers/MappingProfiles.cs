using AutoMapper;
using FIXIT.BLL.Helper.PictureUrlResolver;
using Talabat.Core.Basket;
using Talabat.Core.Entities;
using Talapat.Api.DTOs;
using Talapat.Api.DTOs.Product;

namespace Talapat.Api.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {

            CreateMap<Product, ProductToGetDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver<Product, ProductToGetDto>>());


            CreateMap<CustomerBasketDto, CutomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }

    }
}

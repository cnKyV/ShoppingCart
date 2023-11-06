using AutoMapper;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.Dto;
using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Contract.ResponseModels;
using ShoppingCart.Core.Entity;

namespace ShoppingCart.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductCreateModel, Product>()
            .ForPath(
                dest => dest.Price.TotalPrice,
                opt => opt.MapFrom(src => src.Price))
            .ReverseMap();

        CreateMap<Cart, DisplayCartResponseModel>()
            .ForPath(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Price.DiscountedPrice))
            .ForPath(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.Price.TotalDiscount))
            .ForPath(dest => dest.AppliedPromotionId, opt => opt.MapFrom(src => src.ActivePromotion.PromotionId))
            .ReverseMap();

        CreateMap<Product, ProductDto>()
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price.TotalPrice))
            .ForMember(dest => dest.VasProducts, opt => opt.MapFrom(src => src.VasProducts))
            .ReverseMap();

        CreateMap<VasProduct, VasProductDto>()
            .ForMember(dest => dest.VasItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.VasCategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.VasSellerId, opt => opt.MapFrom(src => src.SellerId))
            .ReverseMap();        
        
        CreateMap<VasProduct, VasProductInsertRequestModel>()
            .ForMember(dest => dest.VasItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.VasCategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.VasSellerId, opt => opt.MapFrom(src => src.SellerId))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.AppliedItemId))
            .ReverseMap();

        CreateMap<ProductInsertRequestModel, ProductCreateModel>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.CategoryId))
            .ReverseMap();

    }
    
}
using AutoMapper;
using eCommerce.BLL.DTO;
using eCommerce.DAL.Entity;

namespace eCommerce.BLL.Mappers;

public class MapProductResponse : Profile
{
    public MapProductResponse()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductId, opt =>
                opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt =>
                opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt =>
                opt.MapFrom(src => src.QuantityInStock));
        
        CreateMap<ProductAddRequest, Product>()
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt =>
                opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt =>
                opt.MapFrom(src => src.QuantityInStock));
        
        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(dest => dest.ProductId, opt =>
                opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt =>
                opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt =>
                opt.MapFrom(src => src.QuantityInStock));
    }
}
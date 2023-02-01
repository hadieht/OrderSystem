using Application.Order.Queries.GetOrder;
using Application.Order.Queries.GetOrdersList;
using Application.Product.Queries;
using AutoMapper;

namespace API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Product, GetProductsResponse>();

        CreateMap<Domain.Entities.OrderItem, GetOrderItemResponse>()
             .ForMember(dest => dest.Product, opts => opts.MapFrom(src => src.Product.ProductType));

        CreateMap<Domain.Entities.Order, GetOrderResponse>()
             .ForMember(dest => dest.CustomerEmail, opts => opts.MapFrom(src => src.CustomerEmail.Value))
             .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.Address.PostalCode))
             .ForMember(dest => dest.HouseNumber, opts => opts.MapFrom(src => src.Address.HouseNumber))
             .ForMember(dest => dest.AddressExtra, opts => opts.MapFrom(src => src.Address.Extra))
             .ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => src.OrderDate.ToLocalTime().ToShortDateString()))
             .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
             .ForMember(dest => dest.BinWidth, m => m.Ignore());


        CreateMap<Domain.Entities.Order, GetOrderListResponse>()
          .ForMember(dest => dest.CustomerEmail, opts => opts.MapFrom(src => src.CustomerEmail.Value))
          .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.Address.PostalCode))
          .ForMember(dest => dest.HouseNumber, opts => opts.MapFrom(src => src.Address.HouseNumber))
          .ForMember(dest => dest.AddressExtra, opts => opts.MapFrom(src => src.Address.Extra))
          .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
          .ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => src.OrderDate.ToLocalTime().ToShortDateString()));

    }
}

using AutoMapper;
using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Infra.Repositories.EntitiesDb;

namespace ChallengeAutoGlass.Infra.Repositories
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductEntitieDb, Product>()
                .ForMember(dest => dest.ProductCode, m => m.MapFrom(src => src.product_code))
                .ForMember(dest => dest.Description, m => m.MapFrom(src => src.description))
                .ForMember(dest => dest.Status, m => m.MapFrom(src => src.status))
                .ForMember(dest => dest.FabricateDate, m => m.MapFrom(src => src.fabricate_date))
                .ForMember(dest => dest.ValidityteDate, m => m.MapFrom(src => src.validity_date))
                .ForMember(dest => dest.ProviderCode, m => m.MapFrom(src => src.provider_code))
                .ForMember(dest => dest.ProviderDescription, m => m.MapFrom(src => src.provider_description))
                .ForMember(dest => dest.CNPJ, m => m.MapFrom(src => src.CNPJ));
        }
    }
}

using AutoMapper;
using VkMarketParser.Core.VkMarket;
using VkNet.Model;

namespace VkMarketParser.Core;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Market, Product>()
            .ForMember(p => p.Name, cfg => cfg.MapFrom(m => m.Title))
            .ForMember(p => p.Link, cfg => cfg.MapFrom(m => $"https://vk.com/market{m.OwnerId}_{m.Id}"))
            .ForMember(p => p.Description, cfg => cfg.MapFrom(m => m.Description))
            .ForMember(p => p.Images, cfg =>
            {
                cfg.PreCondition(m =>  m.Photos is not null && m.Photos.Any() && m.Photos.Select(p => p.Sizes).Any());
                cfg.MapFrom(m => m.Photos.Select(p => p.Sizes.Last().Url.AbsoluteUri).ToList());
            })
            .ForMember(p => p.Price, cfg => cfg.MapFrom(m => $"{m.Price.Amount / 100.0:N2}"));
        
        CreateMap<VkMarketClientConfiguration, VkMarketClientConfiguration>()
            .ForAllMembers(cfg => cfg.Condition((src, dest, srcMember) => src.Login == dest.Login && src.Password == dest.Password && src.AppId == dest.AppId));
    }
}
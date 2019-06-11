using System.Linq;
using AutoMapper;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Mappings
{
    public class RouteMapping : BaseMapping
    {
        public override void Map(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Route, RouteDto>()
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.Routes, opt => opt.MapFrom(src => src.Routes.Select(item => item.Route)))
                .ForMember(dest => dest.Distances, opt => opt.MapFrom(src => src.Routes.Select(item => item.Distance)));
        }
    }
}

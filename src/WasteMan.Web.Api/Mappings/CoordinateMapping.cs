using AutoMapper;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Mappings
{
    public class CoordinateMapping : BaseMapping
    {
        public override void Map(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Coordinate, CoordinateDto>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Longitude));
        }
    }
}

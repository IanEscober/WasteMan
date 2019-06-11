using System;
using AutoMapper;
using WasteMan.Common.Data;
using WasteMan.Common.Data.Enums;
using WasteMan.Redis.Data;

namespace WasteMan.Web.Api.Mappings
{
    public class GarbageBinMapping : BaseMapping
    {
        public override void Map(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<GarbageBin, GarbageBinCacheDto>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.Longitude))
                .ForMember(dest => dest.LidState, opt => opt.MapFrom(src => src.LidSate))
                .ForMember(dest => dest.LidStateTimeStamp, opt => opt.MapFrom(src => src.LidStateTimeStamp))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
            mapper.CreateMap<GarbageBin, GarbageBinDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.LidState, opt => opt.MapFrom(src => src.LidSate))
                .ForMember(dest => dest.LidStateTimeStamp, opt => opt.MapFrom(src => src.LidStateTimeStamp))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Ticks, opt => opt.MapFrom(src => src.Status.Keys))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Status.Values));
            mapper.CreateMap<LidStates, string>().ConvertUsing(src => src.ToString());
            mapper.CreateMap<TimeSpan, string>().ConvertUsing(src => new DateTime(src.Ticks).ToString("h:mm:ss tt"));
        }
    }
}

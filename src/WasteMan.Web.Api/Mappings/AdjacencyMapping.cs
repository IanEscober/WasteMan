using System.Linq;
using AutoMapper;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Mappings
{
    public class AdjacencyMapping : BaseMapping
    {
        public override void Map(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Adjacency, AdjacencyDto>()
                .ForMember(dest => dest.Vertex, opt => opt.MapFrom(src => src.Vertex))
                .ForMember(dest => dest.Neighbors, opt => opt.MapFrom(src => src.Neighbors.Select(item => item.Neighbor)))
                .ForMember(dest => dest.Weights, opt => opt.MapFrom(src => src.Neighbors.Select(item => item.Weight)));
        }
    }
}

using AutoMapper;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Mappings
{
    public class ResultMapping : BaseMapping
    {
        public override void Map(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Result, ResultDto>();
        }
    }
}

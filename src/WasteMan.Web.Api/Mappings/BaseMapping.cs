using AutoMapper;

namespace WasteMan.Web.Api.Mappings
{
    public abstract class BaseMapping
    {
        public abstract void Map(IMapperConfigurationExpression mapper);
    }
}

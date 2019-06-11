using System.Collections.Generic;
using AutoMapper;
using WasteMan.Web.Api.Mappings;

namespace WasteMan.Web.Api.Registrations
{
    public class MappingRegistry
    {
        private List<BaseMapping> mappings;
        public MappingRegistry()
        {
            mappings = new List<BaseMapping>();
        }

        public MappingRegistry Register(BaseMapping baseMapping)
        {
            mappings.Add(baseMapping);
            return this;
        }

        public void Map()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.AllowNullCollections = true;
                foreach (var map in mappings)
                {
                    map.Map(mapper);
                }
            });
        }



    }
}

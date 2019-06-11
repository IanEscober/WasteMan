using System;
using System.Collections.Generic;
using ProtoBuf;
using WasteMan.Common.Data.Enums;

namespace WasteMan.Redis.Data
{
    [ProtoContract]
    public class GarbageBinCacheDto
    {
        [ProtoMember(1)]
        public float? Latitude { get; set; }
        [ProtoMember(2)]
        public float? Longitude { get; set; }
        [ProtoMember(3)]
        public LidStates LidState { get; set; }
        [ProtoMember(4)]
        public TimeSpan LidStateTimeStamp { get; set; }
        [ProtoMember(5)]
        public float Level { get; set; }
        [ProtoMember(6)]
        public IDictionary<TimeSpan, float> Status { get; set; }
    }
}

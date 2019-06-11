using System;

namespace WasteMan.Common.Extensions
{
    public static class TimeSpanRounder
    {
        public static TimeSpan RoundToNearestSecond(this TimeSpan a)
        {
            TimeSpan roundTo = TimeSpan.FromSeconds(1);
            long ticks = (long)(Math.Round(a.Ticks / (double)roundTo.Ticks) * roundTo.Ticks);
            return new TimeSpan(ticks);
        }
    }
}

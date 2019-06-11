using System;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm.Core
{
    internal static class Haversine
    {
        public static float DistanceTo(this Coordinate source, Coordinate destination)
        {
            const float EARTH_RADIUS = 6371 * 1000; // in meters

            var deltaLatitude = (source.Latitude - destination.Latitude).ToRadians();
            var deltaLongitude = (source.Longitude - destination.Longitude).ToRadians();

            var a = (Math.Pow(Math.Sin(deltaLatitude / 2), 2) + 
                            Math.Cos(source.Latitude.ToRadians()) * 
                            Math.Cos(destination.Latitude.ToRadians()) * 
                            Math.Pow(Math.Sin(deltaLongitude / 2), 2));
            var b = (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));


            var distance = EARTH_RADIUS * b;

            var roundedDistance = (float)Math.Round(distance);

            return roundedDistance;
        }

        private static float ToRadians(this float value) => value * (float)(Math.PI / 180);
    }
}

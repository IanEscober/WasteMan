using System;
using System.Linq;
using System.Collections.Generic;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm.Singletons
{
    internal sealed class IntersectionPoints
    {
        private static readonly Lazy<IntersectionPoints> lazy =
                new Lazy<IntersectionPoints>(() => new IntersectionPoints());

        public static IntersectionPoints Instance => lazy.Value;

        private static readonly List<Point> Points = new List<Point>
        {
            new Point { Name = "A", Location = new Coordinate { Latitude = 14.643468f, Longitude = 121.004891f } },
            new Point { Name = "B", Location = new Coordinate { Latitude = 14.643224f, Longitude = 121.002805f } },
            new Point { Name = "C", Location = new Coordinate { Latitude = 14.642872f, Longitude = 121.000337f } },
            new Point { Name = "D", Location = new Coordinate { Latitude = 14.642731f, Longitude = 120.998954f } },
            new Point { Name = "E", Location = new Coordinate { Latitude = 14.642559f, Longitude = 120.997674f } },

            new Point { Name = "F", Location = new Coordinate { Latitude = 14.642145f, Longitude = 121.004999f } },
            new Point { Name = "G", Location = new Coordinate { Latitude = 14.641897f, Longitude = 121.002980f } },
            new Point { Name = "H", Location = new Coordinate { Latitude = 14.641544f, Longitude = 121.000505f } },
            new Point { Name = "I", Location = new Coordinate { Latitude = 14.641456f, Longitude = 120.999197f } },
            new Point { Name = "I*", Location = new Coordinate { Latitude = 14.641395f, Longitude = 120.998498f } },
            new Point { Name = "J", Location = new Coordinate { Latitude = 14.641322f, Longitude = 120.997843f } },

            new Point { Name = "K", Location = new Coordinate { Latitude = 14.640927f, Longitude = 120.995453f } },
            new Point { Name = "L", Location = new Coordinate { Latitude = 14.640276f, Longitude = 121.005260f } },
            new Point { Name = "M", Location = new Coordinate { Latitude = 14.640015f, Longitude = 121.003259f } },
            new Point { Name = "N", Location = new Coordinate { Latitude = 14.639644f, Longitude = 121.000785f } },
            new Point { Name = "O", Location = new Coordinate { Latitude = 14.639633f, Longitude = 121.005327f } },

            new Point { Name = "P", Location = new Coordinate { Latitude = 14.639389f, Longitude = 121.003346f } },
            new Point { Name = "Q", Location = new Coordinate { Latitude = 14.639049f, Longitude = 121.000862f } },
            new Point { Name = "R", Location = new Coordinate { Latitude = 14.638840f, Longitude = 120.999535f } },
            new Point { Name = "R*", Location = new Coordinate { Latitude = 14.638793f, Longitude = 120.998877f } },
            new Point { Name = "S", Location = new Coordinate { Latitude = 14.638686f, Longitude = 120.998219f } },
            new Point { Name = "T", Location = new Coordinate { Latitude = 14.638376f, Longitude = 120.995789f } },

            new Point { Name = "U", Location = new Coordinate { Latitude = 14.638925f, Longitude = 121.005412f } },
            new Point { Name = "V", Location = new Coordinate { Latitude = 14.638665f, Longitude = 121.003453f } },
            new Point { Name = "W", Location = new Coordinate { Latitude = 14.638337f, Longitude = 121.000946f } },
            new Point { Name = "X", Location = new Coordinate { Latitude = 14.638003f, Longitude = 120.998315f } },
            new Point { Name = "Y", Location = new Coordinate { Latitude = 14.637682f, Longitude = 120.995924f } }
        };

        public List<Point> Get() => Points;

        public Point Get(string name) => Points.Where(point => point.Name == name).First();
    }
}

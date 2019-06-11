using WasteMan.Common.Data;

namespace WasteMan.MQTT.Parsers
{
    internal static class PayloadParsers
    {
        public static Coordinate ToCoordinate(this string input)
        {
            var coordinate = input.Split(',');
            return new Coordinate { Latitude = float.Parse(coordinate[0]), Longitude = float.Parse(coordinate[1]) };
        }

        public static (string Level, string LidState) ToUltraGyro(this string input)
        {
            var ultraGyro = input.Split(',');
            return (ultraGyro[0], ultraGyro[1]);
        }
    }
}

using System;
using System.Collections.Generic;
using MQTTnet;
using WasteMan.Common.Extensions;
using WasteMan.Common.Data;
using WasteMan.Common.Data.Enums;
using WasteMan.Common.Helpers;
using WasteMan.MQTT.Data.Enums;

namespace WasteMan.MQTT.Parsers
{
    internal static class MessageParser
    {
        public static GarbageBin ToGarbageBin(this MqttApplicationMessageReceivedEventArgs e)
        {
            var (name, sensor) = e.ApplicationMessage.ToTopic();
            var payload = e.ApplicationMessage.ConvertPayloadToString();
            var bin = new GarbageBin { Name = name };

            switch (sensor)
            {
                case nameof(Sensor.UltraGyro):
                    {
                        var (level, lidState) = payload.ToUltraGyro();
                        bin
                            .ProccesLidState(lidState)
                            .ProcessLevel(level)
                            .ProcessStatus();
                        break;
                    }
                case nameof(Sensor.GPS):
                    {
                        bin.Location = payload.ToCoordinate();
                        break;
                    }
            }

            return bin;
        }

        private static GarbageBin ProccesLidState(this GarbageBin bin, string lidState)
        {
            switch (lidState)
            {
                case nameof(LidStates.Closed):
                    {
                        bin.LidSate = LidStates.Closed;
                        break;
                    }
                case nameof(LidStates.Open):
                    {
                        bin.LidSate = LidStates.Open;
                        break;
                    }
                case nameof(LidStates.Overflow):
                    {
                        bin.LidSate = LidStates.Overflow;
                        break;
                    }
                case nameof(LidStates.Partial):
                    {
                        bin.LidSate = LidStates.Partial;
                        break;
                    }
                default:
                    throw new Exception("Cannot Identify Lid State");
            }

            bin.LidStateTimeStamp = DateTime.Now.TimeOfDay.RoundToNearestSecond();
            return bin;
        }

        private static GarbageBin ProcessLevel(this GarbageBin bin, string level)
        {
            float.TryParse(level, out var resLevel);
            bin.Level = resLevel.IsLevelValid() ? resLevel : throw new ArgumentOutOfRangeException("Level");
            return bin;
        }

        private static GarbageBin ProcessStatus(this GarbageBin bin)
        {
            bin.Status = bin.LidSate.IsLidStateValid()
                ? new Dictionary<TimeSpan, float> { { DateTime.Now.TimeOfDay.RoundToNearestSecond(), bin.Level } }
                : null;
            return bin;
        }      
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Common.Data.Enums;
using WasteMan.Common.Data.Structs;
using WasteMan.Common.Extensions;

namespace WasteMan.Common.Helpers
{
    public static class GarbageBinHelper
    {
        public static bool IsLevelValid(this float level) =>
            level >= Constants.EMPTY_LEVEL && level <= Constants.OVERFLOW_LEVEL
            ? true
            : false;

        public static bool IsLidStateValid(this LidStates lidState) =>
            lidState == LidStates.Closed || lidState == LidStates.Overflow
            ? true
            : false;

        public static IEnumerable<GarbageBin> Filter(this IEnumerable<GarbageBin> bins)
        {
            bins = bins?.Where(bin =>
                bin.Location != null);

            var properBins = bins?.Where(bin =>
                bin.LidSate.IsLidStateValid() &&
                bin.Status?.Last().Value >= Constants.FULL_LEVEL);

            var improperBins = bins?.Where(bin =>
                !bin.LidSate.IsLidStateValid() &&
                DateTime.Now.TimeOfDay.RoundToNearestSecond().Subtract(bin.LidStateTimeStamp)
                > TimeSpan.FromHours(Constants.STUCK_HOUR));

            if (properBins is null && improperBins is null)
            {
                return null;
            }
            else if (properBins is null)
            {
                return improperBins.OrderBy(bin => bin.Name);
            }
            else if (improperBins is null)
            {
                return properBins.OrderBy(bin => bin.Name);
            }
            else
            {
                return (properBins.Concat(improperBins)).OrderBy(bin => bin.Name);
            }
        }

        public static GarbageBin Link(this GarbageBin bin, GarbageBin prevBin)
        {
            if (bin.Status != null || !bin.LidSate.IsLidStateValid())
            {
                bin.Location = prevBin.Location;
                LinkStatus(ref bin, prevBin);
                LinkLidStateTimeStamp(ref bin, prevBin);
            }
            else if (bin.Location != null)
            {
                bin.LidSate = prevBin.LidSate;
                bin.LidStateTimeStamp = prevBin.LidStateTimeStamp;
                bin.Level = prevBin.Level;
                bin.Status = prevBin.Status;
            }
            else 
            {
                throw new Exception("Payload parsing failed");
            }

            return bin;
        }

        private static void LinkStatus(ref GarbageBin bin, GarbageBin prevBin)
        {
            if (prevBin.Status != null)
            {
                var canConcat =
                    bin.Status != null &&
                    bin.Status.Keys.Last() != prevBin.Status.Keys.Last();

                if (canConcat)
                {
                    bin.Status = bin.Status
                        .Concat(prevBin.Status)
                        .OrderBy(sortBy => sortBy.Key)
                        .ToDictionary(keySelector => keySelector.Key, valueSelector => valueSelector.Value);
                }
                else
                {
                    bin.Status = prevBin.Status;
                }
            }
        }

        private static void LinkLidStateTimeStamp(ref GarbageBin bin, GarbageBin prevBin)
        {
            var isSameLidState = bin.LidSate == prevBin.LidSate;
            var isValidLidState = bin.LidSate.IsLidStateValid();

            if (prevBin.Status is null)
            {
                if (!isValidLidState && isSameLidState)
                {
                    if (prevBin.LidStateTimeStamp != TimeSpan.Zero)
                    {
                        bin.LidStateTimeStamp = prevBin.LidStateTimeStamp;
                    }
                }
            }
            else
            {
                if (isSameLidState)
                {
                    bin.LidStateTimeStamp = prevBin.LidStateTimeStamp;
                }
            }
        }
    }
}

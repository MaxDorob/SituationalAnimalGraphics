using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    internal static class Utils
    {
        public static Season GetSeason(Map map, long tick)
        {
            if (map == null)
            {
                Log.Warning("Map is null");
            }
            float latitude = (map != null) ? Find.WorldGrid.LongLatOf(map.Tile).y : 0f;
            float longitude = (map != null) ? Find.WorldGrid.LongLatOf(map.Tile).x : 0f;
            return GenDate.Season(tick, latitude, longitude);
        }
    }
}

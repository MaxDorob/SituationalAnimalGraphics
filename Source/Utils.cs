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
        internal static bool TryGetSeasonalGraphic(this Pawn pawn, out GraphicData result, out GraphicData dessicated, out EffecterDef effecter)
        {
            if (pawn.ageTracker.CurKindLifeStage is SeasonalGraphics seasonalGraphics) {
                var map = pawn.MapHeld ?? pawn.Corpse?.MapHeld;
                var pregnancy = pawn.health.hediffSet.GetFirstHediff<Hediff_Pregnant>();
                var pregnancyDay = pregnancy == null ? -1 : pawn.RaceProps.gestationPeriodDays * pregnancy.GestationProgress;
                var countOfYoung = pregnancy == null ? -1 : 1;
                var season = Utils.GetSeason(map, Find.TickManager.TicksAbs);
                var day = GenLocalDate.DayOfSeason(pawn) + 1;
                bool tamed = pawn.Faction != null;
                bool female = pawn.gender == Gender.Female;
                var resourcePercent = pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f;
                if (seasonalGraphics.TryGetGraphic(season, day, tamed, female, resourcePercent, countOfYoung, (int)pregnancyDay, out result, out dessicated, out effecter))
                {
                    return true;
                }
            }
            result = null;
            dessicated = null;
            effecter = null;
            return false;
        }
    }
}

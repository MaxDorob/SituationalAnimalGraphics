using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    internal class AnimalGraphicsWatcher : MapComponent
    {
        public AnimalGraphicsWatcher(Map map) : base(map)
        {
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            if (!map.IsHashIntervalTick(900))
            {
                return;
            }
            foreach (var pawn in map.mapPawns.AllPawnsSpawned.Where(x => !x.Dead && x.RaceProps.Animal && x.ageTracker.CurKindLifeStage is SeasonalGraphics))
            {
                pawn.Drawer.renderer.renderTree.SetDirty();
                if ((pawn.ageTracker.CurKindLifeStage as SeasonalGraphics).TryGetGraphic(Utils.GetSeason(map, Find.TickManager.TicksAbs), GenLocalDate.DayOfSeason(pawn) + 1, pawn.Faction != null, pawn.gender == Gender.Female, pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f, out _, out _, out var effecter))
                {
                    effecter?.Spawn(pawn.Position, map);
                }
            }
        }
    }
}

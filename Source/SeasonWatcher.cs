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
    internal class SeasonWatcher : MapComponent
    {
        public SeasonWatcher(Map map) : base(map)
        {
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            foreach (var pawn in map.mapPawns.AllPawnsSpawned.Where(x=>!x.Dead && x.RaceProps.Animal && x.ageTracker.CurKindLifeStage is SeasonalGraphicsExtension))
            {
                pawn.Drawer.renderer.renderTree.SetDirty();
            }
        }
    }
}

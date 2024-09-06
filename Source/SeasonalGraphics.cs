using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    public class SeasonalGraphics : PawnKindLifeStage
    {
        public SeasonGraphic summer;
        public SeasonGraphic autumn;
        public SeasonGraphic winter;
        public SeasonGraphic spring;
        public bool TryGetGraphic(Season season, bool tamed, float resourcePercent, out GraphicData result, out EffecterDef effecter)
        {
            switch (season)
            {
                case Season.Spring:
                    if(spring?.TryGetGraphicData(tamed, resourcePercent, out result, out effecter) ?? false)
                    {
                        return true;
                    }
                    goto case Season.Winter;
                case Season.PermanentWinter:
                case Season.Winter:
                    if (winter?.TryGetGraphicData(tamed, resourcePercent, out result, out effecter) ?? false)
                    {
                        return true;
                    }
                    goto case Season.Fall;
                case Season.Fall:
                    if (autumn?.TryGetGraphicData(tamed, resourcePercent, out result, out effecter) ?? false)
                    {
                        return true;
                    }
                    goto case Season.Summer;
                case Season.PermanentSummer:
                case Season.Summer:
                    if (summer?.TryGetGraphicData(tamed, resourcePercent, out result, out effecter) ?? false)
                    {
                        return true;
                    }
                    break;
            }
            result = null;
            effecter = null;
            return false;
        }
    }
}

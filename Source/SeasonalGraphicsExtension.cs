using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    public class SeasonalGraphicsExtension : PawnKindLifeStage
    {
        public SeasonGraphic summer;
        public SeasonGraphic autumn;
        public SeasonGraphic winter;
        public SeasonGraphic spring;
        public GraphicData GetGraphic(Season season, bool tamed, float resourcePercent)
        {
            GraphicData result = null;
            switch (season)
            {
                case Season.Spring:
                    if(spring?.TryGetGraphicData(tamed, resourcePercent, out result) ?? false)
                    {
                        return result;
                    }
                    goto case Season.Winter;
                case Season.PermanentWinter:
                case Season.Winter:
                    if (winter?.TryGetGraphicData(tamed, resourcePercent, out result) ?? false)
                    {
                        return result;
                    }
                    goto case Season.Fall;
                case Season.Fall:
                    if (autumn?.TryGetGraphicData(tamed, resourcePercent, out result) ?? false)
                    {
                        return result;
                    }
                    goto case Season.Summer;
                case Season.PermanentSummer:
                case Season.Summer:
                    if (summer?.TryGetGraphicData(tamed, resourcePercent, out result) ?? false)
                    {
                        return result;
                    }
                    break;
            }
            return result;
        }
    }
}

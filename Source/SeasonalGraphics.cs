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
        public SeasonGraphic fall;
        public SeasonGraphic winter;
        public SeasonGraphic spring;
        private IEnumerable<SeasonGraphic> GraphicsForSeason(Season season)
        {
            switch (season)
            {
                case Season.Spring:
                    if (spring != null)
                    {
                        yield return spring;
                    }
                    goto case Season.Winter;
                case Season.PermanentWinter:
                case Season.Winter:
                    if (winter != null)
                    {
                        yield return winter;
                    }
                    goto case Season.Fall;
                case Season.Fall:
                    if (fall != null)
                    {
                        yield return fall;
                    }
                    goto case Season.Summer;
                case Season.PermanentSummer:
                case Season.Summer:
                    if (summer != null)
                    {
                        yield return summer;
                    }
                    break;
            }
        }
        public bool TryGetGraphic(Season season, bool tamed, bool female, float resourcePercent, out GraphicData result, out GraphicData dessicated, out EffecterDef effecter)
        {
            foreach (var seasonGraphic in GraphicsForSeason(season))
            {
                if (seasonGraphic.TryGetGraphicData(tamed, female, resourcePercent, out result, out dessicated, out effecter))
                {
                    return true;
                }
            }
            result = null;
            effecter = null;
            dessicated = null;
            return false;
        }
    }
}

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

        public List<SubSeasonGraphics> summerGraphics;
        public List<SubSeasonGraphics> fallGraphics;
        public List<SubSeasonGraphics> winterGraphics;
        public List<SubSeasonGraphics> springGraphics;

        private SeasonGraphic GetFromSubSeason(IEnumerable<SubSeasonGraphics> subSeasonGraphics, int day)
        {
            if (subSeasonGraphics.EnumerableNullOrEmpty())
            {
                return null;
            }
            return subSeasonGraphics.OrderByDescending(x => x.day).FirstOrDefault(x => x.day < day)?.graphics;
        }
        private IEnumerable<SeasonGraphic> GraphicsForSeason(Season season, int day)
        {
            SeasonGraphic subGraphics;
            switch (season)
            {

                case Season.PermanentSummer:
                    if (!summerGraphics.NullOrEmpty())
                    {
                        yield return summerGraphics.OrderBy(x=> x.day).LastOrDefault().graphics;
                        yield break;
                    }
                    break;
                case Season.PermanentWinter:
                    if (!winterGraphics.NullOrEmpty())
                    {
                        yield return winterGraphics.LastOrDefault().graphics;
                        yield break;
                    }
                    break;
            }
            switch (season)
            {
                case Season.Spring:
                    subGraphics = GetFromSubSeason(springGraphics, day);
                    if (subGraphics != null)
                    {
                        yield return subGraphics;
                    }
                    else if (spring != null)
                    {
                        yield return spring;
                    }
                    goto case Season.Winter;
                case Season.PermanentWinter:
                case Season.Winter:
                    subGraphics = GetFromSubSeason(winterGraphics, day);
                    if (subGraphics != null)
                    {
                        yield return subGraphics;
                    }
                    else if (winter != null)
                    {
                        yield return winter;
                    }
                    goto case Season.Fall;
                case Season.Fall:
                    subGraphics = GetFromSubSeason(fallGraphics, day);
                    if (subGraphics != null)
                    {
                        yield return subGraphics;
                    }
                    else if (fall != null)
                    {
                        yield return fall;
                    }
                    goto case Season.Summer;
                case Season.PermanentSummer:
                case Season.Summer:
                    subGraphics = GetFromSubSeason(summerGraphics, day);
                    if (subGraphics != null)
                    {
                        yield return subGraphics;
                    }
                    else if (summer != null)
                    {
                        yield return summer;
                    }
                    break;
            }
        }
        public bool TryGetGraphic(Season season, int day, bool tamed, bool female, float resourcePercent, int youngCount, int pregnancyDay, out GraphicData result, out GraphicData dessicated, out EffecterDef effecter)
        {
            foreach (var seasonGraphic in GraphicsForSeason(season, day))
            {
                if (seasonGraphic.TryGetGraphicData(tamed, female, resourcePercent, youngCount, pregnancyDay, out result, out dessicated, out effecter))
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

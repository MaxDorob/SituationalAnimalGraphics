using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    public class SeasonGraphic
    {
        public EffecterDef resourceGathered;
        public GraphicData resourceGatheredGraphicData;
        public GraphicData resourceGatheredGraphicDataFemale;
        public float resourceGatheredActiveUntil = 0.9f;
        public EffecterDef tamed;
        public GraphicData tamedGraphicData;
        public GraphicData tamedGraphicDataFemale;
        public EffecterDef defaultGraphicChangedEffect;
        public GraphicData defaultGraphicData;
        public GraphicData defaultGraphicDataFemale;
        public GraphicData dessicatedBodyGraphicData;
        public GraphicData dessicatedBodyGraphicDataFemale;
        public List<PregnancyGraphicsByCount> pregnancyGraphicsByCount;
        public bool TryGetGraphicData(bool tamed, bool female, float resourcePercent, int youngCount, int pregnancyDay, out GraphicData graphicData, out GraphicData dessicatedBodyGraphicData, out EffecterDef effecter)
        {
            dessicatedBodyGraphicData = female ? dessicatedBodyGraphicDataFemale ?? this.dessicatedBodyGraphicData : this.dessicatedBodyGraphicData;
            if (pregnancyDay >= 0 && pregnancyGraphicsByCount != null && pregnancyGraphicsByCount.Count > 0)
            {
                effecter = defaultGraphicChangedEffect;
                var pregancyState = pregnancyGraphicsByCount[Math.Min(youngCount - 1, pregnancyGraphicsByCount.Count - 1)].graphicsByDays?.LastOrDefault(x => x.day <= pregnancyDay);
                graphicData = pregancyState?.graphicData;
                if (pregancyState != null)
                {
                    dessicatedBodyGraphicData = pregancyState.dessicatedGraphicData;
                }
                return true;
            }
            if (resourceGatheredGraphicData != null && resourcePercent < resourceGatheredActiveUntil)
            {
                effecter = resourceGathered ?? defaultGraphicChangedEffect;
                graphicData = female ? resourceGatheredGraphicDataFemale ?? resourceGatheredGraphicData : resourceGatheredGraphicData;
                return true;
            }
            if (tamed && tamedGraphicData != null)
            {
                effecter = this.tamed ?? defaultGraphicChangedEffect;
                graphicData = female ? tamedGraphicDataFemale ?? tamedGraphicData : tamedGraphicData;
                return true;
            }
            effecter = defaultGraphicChangedEffect;
            graphicData = female ? defaultGraphicDataFemale ?? defaultGraphicData : defaultGraphicData;
            return graphicData != null;
        }
    }
}

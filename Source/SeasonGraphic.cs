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
        public float resourceTakenActiveUntil = 0.9f;
        public EffecterDef tamed;
        public GraphicData tamedGraphicData;
        public EffecterDef defaultGraphicChangedEffect;
        public GraphicData defaultGraphicData;

        public bool TryGetGraphicData(bool tamed, float resourcePercent, out GraphicData graphicData, out EffecterDef effecter)
        {
            if (resourceGatheredGraphicData != null && resourcePercent < resourceTakenActiveUntil)
            {
                effecter = resourceGathered ?? defaultGraphicChangedEffect;
                graphicData = resourceGatheredGraphicData;
                return true;
            }
            if (tamed && tamedGraphicData != null)
            {
                effecter = this.tamed ?? defaultGraphicChangedEffect;
                graphicData = tamedGraphicData;
                return true;
            }
            effecter = defaultGraphicChangedEffect;
            graphicData = defaultGraphicData;
            return graphicData != null;
        }
    }
}

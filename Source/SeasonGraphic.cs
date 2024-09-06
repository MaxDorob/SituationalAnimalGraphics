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
        public GraphicData resourceTakenGraphicData;
        public float resourceTakenActiveUntil = 0.9f;
        public GraphicData tamedGraphicData;
        public GraphicData defaultGraphicData;

        public bool TryGetGraphicData(bool tamed, float resourcePercent, out GraphicData graphicData)
        {
            if (resourceTakenGraphicData != null && resourcePercent < resourceTakenActiveUntil)
            {
                graphicData = resourceTakenGraphicData;
                return true;
            }
            if (tamed && tamedGraphicData != null)
            {
                graphicData = tamedGraphicData;
                return true;
            }
            graphicData = defaultGraphicData;
            return graphicData != null;
        }
    }
}

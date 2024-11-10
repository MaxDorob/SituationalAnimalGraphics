using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    public class PregnancyGraphicsByCount
    {
        public List<PregnancyGraphicsByDays> graphicsByDays;
    }

    public class PregnancyGraphicsByDays
    {
        public int day = 0;
        public GraphicData graphicData;
    }
}

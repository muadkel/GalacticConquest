using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    public class PlanetPopularity
    {
        public int CurrentPopularity;

        public int TotalPopularity;


        public PlanetPopularity()
        {
            //Starts Nuetral
            CurrentPopularity = 0;
            //Base of 100 for popularity to sway from. Will eventually be based on population//
            TotalPopularity = 100;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class GalacticDayManager
    {
        public int CurrentGalacticDay;


        public GalacticDayManager()
        {
            CurrentGalacticDay = 0;
        }




        public void advanceADay()
        {
            CurrentGalacticDay++;
        }



    }
}

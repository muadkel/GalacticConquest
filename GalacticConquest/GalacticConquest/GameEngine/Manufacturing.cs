using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class Manufacturing
    {
        public static string FacilityMFR = "MFR[Facilities]";
        public static string ShipMFR = "MFR[Ships]";
        public static string TroopMFR = "MFR[Troops]";
        public static string VehicleMFR = "";



        public int ResourceCost;
        public int BaseConstructionTime;

        public Manufacturing()
        {
            ResourceCost = 0;
            BaseConstructionTime = 1;
        }


        public Manufacturing(int _ResourceCost, int _BaseConstructionTime)
        {
            ResourceCost = _ResourceCost;
            BaseConstructionTime = _BaseConstructionTime;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    //Space refers to the amount of open area slots are available for development
    public class PlanetSpace
    {
        public int AllSpaces;

        public List<GameEngine.PlayerInvObjs.InvUnit> Facilities;
        public List<GameEngine.PlayerInvObjs.InvUnit> Troops;

        public PlanetSpace()
        {
            AllSpaces = 0;
            Facilities = new List<GameEngine.PlayerInvObjs.InvUnit>();
            Troops = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }

        public PlanetSpace(int defaultSpaces)
        {
            AllSpaces = defaultSpaces;
            Facilities = new List<GameEngine.PlayerInvObjs.InvUnit>();
            Troops = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }


        //Methods
        public int getAvailableSpace()
        {//Facilities take up spaces on the planet. Each planet has a unique amount of spaces to build with.
            return AllSpaces - Facilities.Count;
        }

        public bool hasFacilityMFR()
        {
            bool blnReturn = false;
            foreach (GameEngine.PlayerInvObjs.InvUnit curUnit in Facilities)
            {
                DataCards.Facility curFacility = curUnit.iuFacility;
                if (curFacility.Type == Manufacturing.FacilityMFR)
                    blnReturn = true;

            }
            return blnReturn;
        }

        public bool hasShipMFR()
        {
            bool blnReturn = false;
            foreach (GameEngine.PlayerInvObjs.InvUnit curUnit in Facilities)
            {
                DataCards.Facility curFacility = curUnit.iuFacility;

                if (curFacility.Type == Manufacturing.ShipMFR)
                    blnReturn = true;
            }
            return blnReturn;
        }

        public bool hasTroopsMFR()
        {
            bool blnReturn = false;
            foreach (GameEngine.PlayerInvObjs.InvUnit curUnit in Facilities)
            {
                DataCards.Facility curFacility = curUnit.iuFacility;

                if (curFacility.Type == Manufacturing.TroopMFR)
                    blnReturn = true;
            }
            return blnReturn;
        }

        public bool hasTroops()
        {
            bool blnReturn = false;

            if (Troops.Count > 0)
                blnReturn = true;

            return blnReturn;
        }
    }
}

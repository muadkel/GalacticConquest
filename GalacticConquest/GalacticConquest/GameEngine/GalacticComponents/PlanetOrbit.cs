using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    public class PlanetOrbit
    {
        public List<GameEngine.PlayerInvObjs.InvUnit> StarshipFleetsInOrbit;


        public PlanetOrbit()
        {
        //    AllSpaces = 0;
            StarshipFleetsInOrbit = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }


        //Methods

        public void removeShipFromOrbitByID(int ID)
        {
            for (int i = 0; i < StarshipFleetsInOrbit.Count; i++)
            {
                GameEngine.PlayerInvObjs.InvUnit curIU = StarshipFleetsInOrbit[i];

                if(curIU.id == ID)
                {
                    StarshipFleetsInOrbit.RemoveAt(i);
                }
            }
        }

        public bool hasShipsInOrbit()
        {
            bool rtnHasShips = false;

            if (StarshipFleetsInOrbit.Count > 0)
                rtnHasShips = true;

            return rtnHasShips;
        }
    }
}

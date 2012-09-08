using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.PlayerInvObjs
{
    public class StarshipFleet
    {
        public List<GameEngine.PlayerInvObjs.InvUnit> ShipsInFleet;


        public StarshipFleet()
        {
        //    AllSpaces = 0;
            ShipsInFleet = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }


        //Methods

        public void removeShipFromOrbitByID(int ID)
        {
            for (int i = 0; i < ShipsInFleet.Count; i++)
            {
                GameEngine.PlayerInvObjs.InvUnit curIU = ShipsInFleet[i];

                if(curIU.id == ID)
                {
                    ShipsInFleet.RemoveAt(i);
                }
            }
        }
    }
}

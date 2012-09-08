using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class StartingGameUnits
    {
        //public List<DataCards.Facility> startingFacilities;

        //public List<DataCards.Planet> startingPlanets;

        //public List<DataCards.Ship> startingShips;

        public List<GameEngine.PlayerInvObjs.InvUnit> startingUnits;

        public int startingResources;

        public StartingGameUnits()
        {
            //startingFacilities = new List<DataCards.Facility>();
            //startingPlanets = new List<DataCards.Planet>();
            //startingShips = new List<DataCards.Ship>();


            startingUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();

            startingResources = 0;
        }

    }
}

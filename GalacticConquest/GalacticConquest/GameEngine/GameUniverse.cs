using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class GameUniverse
    {
        public List<DataCards.Galaxy> TheUniverse;

        //Constructor
        public GameUniverse()
        {
            TheUniverse = new List<DataCards.Galaxy>();
        }

        //Methods
        public List<DataCards.Planet> getPlanetListByGalaxyName(string galaxyName)
        {
            List<DataCards.Planet> rtnPlanets = new List<DataCards.Planet>();

            foreach (DataCards.Galaxy curGalaxy in TheUniverse)
            {
                if (curGalaxy.Name == galaxyName)
                {
                    foreach (DataCards.Planet curPlanet in curGalaxy.Planets)
                        rtnPlanets.Add(curPlanet);
                }
            }

            return rtnPlanets;
        }

        public List<DataCards.Planet> getAllPlanetsList()
        {
            List<DataCards.Planet> rtnPlanets = new List<DataCards.Planet>();

            foreach (DataCards.Galaxy curGalaxy in TheUniverse)
            {
                    foreach (DataCards.Planet curPlanet in curGalaxy.Planets)
                        rtnPlanets.Add(curPlanet);
            }

            return rtnPlanets;
        }

        public DataCards.Planet getPlanetByName(string planetName)
        {
            DataCards.Planet rtnPlanet = new DataCards.Planet();

            foreach (DataCards.Galaxy curGalaxy in TheUniverse)
            {
                    foreach (DataCards.Planet curPlanet in curGalaxy.Planets)
                    {
                        if (curPlanet.Name == planetName)
                            rtnPlanet = curPlanet;
                    }
            }

            return rtnPlanet;
        }


        public GameEngine.PlayerInvObjs.InvUnit getInvUnitByID(int id, Model.DataCardType dataCardType)
        {
            GameEngine.PlayerInvObjs.InvUnit rtnInvUnit = new GameEngine.PlayerInvObjs.InvUnit();

            if (dataCardType == GalacticConquest.Model.DataCardType.Facility)
            {
            }
            else if (dataCardType == GalacticConquest.Model.DataCardType.Ship)
            {
                List<DataCards.Planet> allPlanets = this.getAllPlanetsList();

                foreach (DataCards.Planet curPlanet in allPlanets)
                {
                    foreach (GameEngine.PlayerInvObjs.InvUnit curInv in curPlanet.Orbit.StarshipFleetsInOrbit)
                    {
                        if (curInv.id == id)
                        {
                            rtnInvUnit = curInv;
                            break;
                        }
                    }
                }

            }
            else if (dataCardType == GalacticConquest.Model.DataCardType.Troops)
            {
            }
            return rtnInvUnit;
        }


        //Chrono methods (Time based)
        public void advanceGalacticDay(Player currentPlayer)
        {
            int dayInc = 1;//The days to incriment by// Defaults to 1 but maybe we want a multi day advance later
            List<DataCards.Planet> allPlanetsInUniverse = getAllPlanetsList();

            for (int i = 0; i < dayInc; i++)
            {//For each day

                foreach (DataCards.Planet curPlanet in allPlanetsInUniverse)
                {//For each Planet

                    //Give player 1 resources from the planets owned
                    if (curPlanet.Owner.player == currentPlayer)
                        currentPlayer.curGameResourceMang.invRawMaterials += curPlanet.RawMaterialsPerDay;

                    //Check all ships in orbit for transit updates
                    foreach (GameEngine.PlayerInvObjs.InvUnit curShipIU in curPlanet.Orbit.StarshipFleetsInOrbit)
                    {
                        if (curShipIU.transitObj._inTransit)
                            curShipIU.transitObj.updateTransitByDay();
                    }
                }

            }

        }

    }
}

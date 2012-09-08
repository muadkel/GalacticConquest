using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.PlayerInvObjs
{
    public class InvUnit
    {
        public Model.DataCardType invUnitType;

        public string startingLocation;

        public GameEngine.GalacticComponents.Owner FacilityOwner;

        public bool _underConstruction;



        public GameEngine.GalacticComponents.TravelObj transitObj;

        public int _remainingConstructionDays;

        public int id;

        public static int ID = 1;

        public static int getID() { return ID++; }


        //Unit Type Objects
            public DataCards.Facility iuFacility;
            public DataCards.Planet iuPlanet;
            public DataCards.Ship iuShip;
            public DataCards.Troops iuTroops;


            public InvUnit()
            {
                startingLocation = "";
                invUnitType = new Model.DataCardType();
                iuFacility = new DataCards.Facility();
                _underConstruction = false;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = 0;
            }

            public InvUnit(DataCards.Facility invUnitFacility, string _startingPlanetName)
            {
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Facility;
                iuFacility = invUnitFacility;
                _underConstruction = false;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = 0;
                id = getID();
            }


            public InvUnit(DataCards.Ship invUnitShip, string _startingPlanetName)
            {
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Ship;
                iuShip = invUnitShip;
                _underConstruction = false;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = 0;
                id = getID();
            }

            public InvUnit(DataCards.Troops invUnitTroops, string _startingPlanetName)
            {//Create a Troops unit and set it at a planet //Implies that this might mean its a starting unit. Generally they would need a construction.//Possibly loading from a save file in the future :) 
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Troops;
                iuTroops = invUnitTroops;
                _underConstruction = false;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = 0;
                id = getID();
            }


            public InvUnit(DataCards.Planet invUnitPlanet)
            {
                startingLocation = invUnitPlanet.Name;
                invUnitType = Model.DataCardType.Planet;
                iuPlanet = invUnitPlanet;
                _underConstruction = false;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = 0;
                id = getID();
            }


            public void startEntityConstruction(DataCards.Facility invUnitFacility, string _startingPlanetName)
            {
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Facility;
                iuFacility = invUnitFacility;
                _underConstruction = true;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = iuFacility.baseManufactureCost.BaseConstructionTime;
                id = getID();
            }


            public void startEntityConstruction(DataCards.Ship invUnitShip, string _startingPlanetName)
            {
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Ship;
                iuShip = invUnitShip;
                _underConstruction = true;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                _remainingConstructionDays = iuFacility.baseManufactureCost.BaseConstructionTime;
                id = getID();
            }


            public void startEntityConstruction(DataCards.Troops invUnitTroops, string _startingPlanetName)
            {
                startingLocation = _startingPlanetName;
                invUnitType = Model.DataCardType.Troops;
                iuTroops = invUnitTroops;
                _underConstruction = true;
                transitObj = new GameEngine.GalacticComponents.TravelObj();
                //_remainingConstructionDays = iuTroops.baseManufactureCost.BaseConstructionTime;
                _remainingConstructionDays = 1;
                id = getID();
            }
            //public InvUnit(DataCards.Planet invUnitPlanet)
            //{
            //    invUnitType = Model.DataCardType.Planet;
            //    iuPlanet = invUnitPlanet;
            //}
    }
}

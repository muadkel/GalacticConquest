using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.Model
{
    public class StaticDataCards
    {
        public List<DataCards.Faction> _factions;
        public List<DataCards.Facility> _facilities;
        public List<DataCards.Ship> _ships;
        public List<DataCards.Character> _characters;
        public List<DataCards.Troops> _troops;

        public bool _isNull;

        //Constructor
        public StaticDataCards()
        {
            _factions = Model.DataUtilities.getFactionsFromXML();
            _facilities = Model.DataUtilities.getFacilitiesFromXML();
            _ships = Model.DataUtilities.getShipsFromXML();
            _characters = Model.DataUtilities.getCharactersFromXML();
            _troops = Model.DataUtilities.getTroopsFromXML();

            _isNull = false;
        }

        //Methods
        public DataCards.Facility getFacilityByName(string strFacilityName)
        {
            DataCards.Facility rtnFacility = new DataCards.Facility();

            foreach (DataCards.Facility curFacility in _facilities)
            {
                if (curFacility.Name == strFacilityName)
                {
                    rtnFacility = curFacility;
                }
            }
            return rtnFacility;
        }

        public DataCards.Ship getShipByName(string strShipName)
        {
            DataCards.Ship rtnShip = new DataCards.Ship();

            foreach (DataCards.Ship curShip in _ships)
            {
                if (curShip.Name == strShipName)
                {
                    rtnShip = curShip;
                }
            }
            return rtnShip;
        }

        public DataCards.Troops getTroopsByName(string strTroopsName)
        {
            DataCards.Troops rtnTroops = new DataCards.Troops();

            foreach (DataCards.Troops curTroops in _troops)
            {
                if (curTroops.Name == strTroopsName)
                {
                    rtnTroops = curTroops;
                }
            }
            return rtnTroops;
        }
    }
}

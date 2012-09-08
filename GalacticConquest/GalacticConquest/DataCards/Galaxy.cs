using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Galaxy
    {
        private int _id;

        private string _name;
        private Model.Coordinates _universeCoordinates;
        private string _mouseImageAreaCoordinates;


        public string MouseImageAreaCoordinates
        {
            get { return _mouseImageAreaCoordinates; }
            set { _mouseImageAreaCoordinates = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private List<DataCards.Planet> _planets;

        public Model.Coordinates UniverseCoordinates
        {
            get { return _universeCoordinates; }
            set { _universeCoordinates = value; }
        }
	
        public List<DataCards.Planet> Planets
        {
            get { return _planets; }
            //set { _planets = value; }
        }
	

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        //Constructors
        public Galaxy()
        {
            _id = -1;

            _planets = new List<Planet>();
        }

	    //Methods
        public void addPlanet(Planet newPlanet)
        {
            _planets.Add(newPlanet);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Planet
    {
        public string Name;
        public string HeaderText;
        public string Description;

        public GameEngine.GalacticComponents.Owner Owner;

        ////////////////////////////////////////
        //Basic Static(For each Planet) Data
        public double RMRate;
        public GameEngine.GalacticComponents.PlanetSpace GroundSpaces;
        public GameEngine.GalacticComponents.PlanetOrbit Orbit;


        //Dynamic (Player based) Planet Data
        public GameEngine.GalacticComponents.PlanetPopularity Popularity;

        public int RawMaterialsPerDay;//This is kind of static for now. The idea later is that the player would need to refine materials to use them.

        public Planet()
        {
            Name = "";
            HeaderText = "";
            Description = "";

            Popularity = new GameEngine.GalacticComponents.PlanetPopularity();

            RawMaterialsPerDay = 0;

            RMRate = 0.1;

            GroundSpaces = new GameEngine.GalacticComponents.PlanetSpace();
            Orbit = new GameEngine.GalacticComponents.PlanetOrbit();
            Owner = new GameEngine.GalacticComponents.Owner();


        }

        //public Planet(int defaultPlanetSpaces)
        //{
        //    Name = "";
        //    HeaderText = "";
        //    Description = "";

        //    Popularity = new GameEngine.GalacticComponents.PlanetPopularity();

        //    RawMaterialsPile = 0;

        //    RMRate = 0.1;

        //    Spaces = new GameEngine.GalacticComponents.PlanetSpace();
        //    Owner = new GameEngine.GalacticComponents.PlanetOwner();
        //}
	


        //Override methods

        public override string ToString()
        {
            return Name;

            //return base.ToString();
        }
    }
}

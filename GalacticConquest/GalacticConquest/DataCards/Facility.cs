using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Facility : DataCard
    {
        public string Name;
        public string HeaderText;
        public string Type;
        public string Description;
        public string MainImagePath;
        public string FacilityType;


        //Data Attributes    Might move to interface class?


        public GameEngine.Manufacturing baseManufactureCost;


        public Facility()
        {
            Name = "";
            HeaderText = "";
            Description = "";
            MainImagePath = "";
            FacilityType = "";

            baseManufactureCost = new GameEngine.Manufacturing();
        }

    }
}

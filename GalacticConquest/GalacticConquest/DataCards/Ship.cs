using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Ship : GameUnit
    {
        public string ID;

        public string Name;
        public string HeaderText;
        public string Type;
        public string Description;

        public string FullImagePath;
        public string SmallImagePath;

        public string ShipType;

        public int Hyperspeed;
        public int TroopSpaces;

        public Ship()
        {
            ID = "";
            Name = "";
            HeaderText = "";
            Description = "";
            FullImagePath = "";
            SmallImagePath = "";
            ShipType = "";
            Hyperspeed = 0;
            TroopSpaces = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Troops : GameUnit
    {
        public string ID;
        public string Name;
        public string HeaderText;
        public string Type;
        public string Description;
        public string FullImagePath;
        public string SmallImagePath;
        public string TroopType;

        public int Attack;
        public int Health;


        public Troops()
        {
            ID = "";
            Name = "";
            HeaderText = "";
            Description = "";
            FullImagePath = "";
            SmallImagePath = "";
            TroopType = "";
            Attack = 0;
            Health = 0;
        }

    }
}

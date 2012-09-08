using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Character
    {
        public string Name;

        public string HeaderText;

        public string Species;

        public string Description;

        public string MainImagePath;


        public string CharacterType;

        public Character()
        {
            Name = "";
            HeaderText = "";
            Description = "";
            MainImagePath = "";
            CharacterType = "";
        }
    }
}

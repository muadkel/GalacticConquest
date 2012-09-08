using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class DataCard
    {
        public string Name;
        public string HeaderText;
        public string Type;
        public string Description;
        public string MainImagePath;

        public DataCard()
        {
            Name = "";
            HeaderText = "";
            Description = "";
            MainImagePath = "";
        }

    }
}

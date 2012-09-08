using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticConquest.DataCards
{
    public class Faction
    {
        public string ID;
        public string Name;
        public string HeaderText;


        public Faction()
        {
            ID = "";
            Name = "";
            HeaderText = "";
        }

        public Faction(string factionName)
        {
            setFactionValues(factionName);
        }

        //Methods
        public void setFactionValues(string factionName)
        {
            List<Faction> allFactions = Model.DataUtilities.getFactionsFromXML();

            foreach (Faction curFaction in allFactions)
            {
                if (curFaction.Name == factionName)
                {
                    this.ID = curFaction.ID;
                    this.Name = curFaction.Name;
                    this.HeaderText = curFaction.HeaderText;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
	
    }
}

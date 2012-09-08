using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class Player
    {
        public string UserName;

        public DataCards.Faction usersFaction;
        
        public ResourceManager curGameResourceMang;


        public List<GameEngine.PlayerInvObjs.InvUnit> playerUnits;
        
        public Player()
        {
            UserName = "";
            usersFaction = new DataCards.Faction();
            curGameResourceMang = new ResourceManager();

            playerUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }


        public Player(string userName, string factionName, int startingRawMaterials)
        {
            UserName = userName;
            usersFaction = new DataCards.Faction(factionName);
            curGameResourceMang = new ResourceManager();
            curGameResourceMang.invRawMaterials = startingRawMaterials;

            playerUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();
        }

    }
}

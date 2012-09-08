using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.SaveGame
{
    [Serializable]
    public struct SaveGameData
    {
        public string PlayerName;
        //public Vector2 AvatarPosition;
        public int GalacticDay;
        public int RawMaterials;
    }

}

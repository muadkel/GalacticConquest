using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    public class Owner
    {
        public GameEngine.Player player;


        public string Name;
        

        public bool _neutral;


        public Owner()
        {
            Name = "Neutral";
            _neutral = true;
        }

        public Owner(string name)
        {
            Name = name;
            _neutral = false;
        }

        public Owner(GameEngine.Player _player)
        {
            Name = _player.UserName;
            player = _player;
            _neutral = false;
        }

    }
}

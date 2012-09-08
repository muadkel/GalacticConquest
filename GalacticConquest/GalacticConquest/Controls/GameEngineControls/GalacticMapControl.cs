using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.GameEngineControls
{
    public class GalacticMapControl
    {
        public Texture2D menuImageToDisplay;
        public Vector2 menuVector;


        public bool _isNull;
        public bool _isEnabled;


        public GalacticMapControl()
        {
            _isNull = true;
            _isEnabled = false;
            
            menuVector = Vector2.Zero;
            
        }


        public void loadMap(Texture2D galaxyToDisplay, Vector2 vectLoc)
        {
            _isNull = false;
            _isEnabled = true;

            menuImageToDisplay = galaxyToDisplay;

            menuVector = vectLoc;
        }


        public void Disable()
        {
            if (!_isNull)
            {
                _isEnabled = false;
            }
        }

        public void Enable()
        {
            if (!_isNull)
            {
                _isEnabled = true;
            }
        }



        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {
            if (!this._isNull)
            {
                curGameDrawComponents._spriteBatch.Draw(this.menuImageToDisplay, this.menuVector, Color.White);
            }
        }




    }
}

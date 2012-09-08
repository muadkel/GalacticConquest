using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.DrawObj
{
    public class Sprite
    {
        public string Name;

        public Texture2D spriteTexture;

        public Vector2 VectorPosition;

        public bool _isNull;

        public Sprite()
        {
            _isNull = true;
            Name = "";
            VectorPosition = Vector2.Zero;
        }

        public Sprite(string curName, Vector2 curVector, Texture2D curSpriteTexture)
        {
            _isNull = false;
            Name = curName;
            VectorPosition = curVector;
            spriteTexture = curSpriteTexture;
        }


        //Draw Commands
        public void Draw(GameDrawClassComponents curGameDrawClassComponents)
        {
            curGameDrawClassComponents._spriteBatch.Draw(spriteTexture, Model.Utilities.getRectFromTexture2D(spriteTexture,VectorPosition), Color.White);
        }


        //Update Methods
        // MouseClicks
        public bool mouseClicked(GameUpdateClassComponents curGameUpdateComponents)
        {
            bool blnReturn = false;

            if (curGameUpdateComponents._curMouseState.X > this.VectorPosition.X && curGameUpdateComponents._curMouseState.X < (this.VectorPosition.X + this.spriteTexture.Width))
            {
                if (curGameUpdateComponents._curMouseState.Y > this.VectorPosition.Y && curGameUpdateComponents._curMouseState.Y < (this.VectorPosition.Y + this.spriteTexture.Height))
                {
                    blnReturn = true;
                }
            }

            return blnReturn;
        }
    }
}

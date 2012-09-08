using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class FancyButtonControl
    {
        public string ID;

        public string strControlText;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public bool _isNull;


        public FancyButtonControl()
        {
            _isNull = true;
            ID = "";
            vectorPos = Vector2.Zero;
        }

        public FancyButtonControl(string id, Texture2D curImgControl, Vector2 curVectorPos, Rectangle curRect, string curControlText)
        {
            _isNull = false;
            ID = id;

            imgControl = curImgControl;
            vectorPos = curVectorPos;
            rectControl = curRect;

            strControlText = curControlText;

        }


        //Draw Methods

        public void Draw(SpriteBatch spriteBatch, Model.StaticFonts staticFonts)
        {
            int txtXPad = 10;
            int txtYPad = 10;



            Rectangle btnRect = rectControl;

            btnRect.X = (int)vectorPos.X;
            btnRect.Y = (int)vectorPos.Y;

            float textWidth = staticFonts._courierNew.MeasureString(this.strControlText).Length();

            if (textWidth > rectControl.Width)
                rectControl.Width = Convert.ToInt32(textWidth + txtXPad + txtXPad);
            //Display Button Image
            spriteBatch.Draw(this.imgControl, rectControl, Color.White);



            //Display Button Text
            Vector2 textVector = new Vector2(this.vectorPos.X + txtXPad, this.vectorPos.Y + txtYPad);
            spriteBatch.DrawString(staticFonts._courierNew, this.strControlText, textVector, Color.Black);
        }


        //Update Methods
        // MouseClicks
        public bool mouseClicked(GameUpdateClassComponents curGameUpdateComponents)
        {
            bool blnReturn = false;

            if (curGameUpdateComponents._curMouseState.X > this.vectorPos.X && curGameUpdateComponents._curMouseState.X < (this.vectorPos.X + this.rectControl.Width))
            {
                if (curGameUpdateComponents._curMouseState.Y > this.vectorPos.Y && curGameUpdateComponents._curMouseState.Y < (this.vectorPos.Y + this.rectControl.Height))
                {
                    blnReturn = true;
                }
            }

            return blnReturn;
        }

    }
}

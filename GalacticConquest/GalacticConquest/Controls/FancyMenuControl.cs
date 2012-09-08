using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GalacticConquest.Controls
{
    public class FancyMenuControl
    {
        //Properties
        public string ID;

        public string strControlHeader;
        
        public string strControlText;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;


        public List<Controls.FancyButtonControl> menuButtons;

        public List<Controls.FancyRadioButtonsControl> menuRadioButtonControls;


        public bool _isNull;
        


        //Constructors
        public FancyMenuControl()
        {
            _isNull = true;
            ID = "";
            vectorPos = Vector2.Zero;
            menuButtons = new List<FancyButtonControl>();
            menuRadioButtonControls = new List<FancyRadioButtonsControl>();
        }

        public FancyMenuControl(string id, Texture2D curImgControl, Vector2 curVectorPos, Rectangle curRect, string curControlHeader, string curControlText, List<Controls.FancyButtonControl> curMenuButtons)
        {
            _isNull = false;
            ID = id;
            imgControl = curImgControl;
            vectorPos = curVectorPos;
            rectControl = curRect;

            strControlHeader = curControlHeader;
            strControlText = curControlText;


            menuButtons = curMenuButtons;
            menuRadioButtonControls = new List<FancyRadioButtonsControl>();
        }

        public FancyMenuControl(string id, Texture2D curImgControl, Vector2 curVectorPos, Rectangle curRect, string curControlHeader, string curControlText, List<Controls.FancyButtonControl> curMenuButtons, List<Controls.FancyRadioButtonsControl> curMenuRadioButtonControls)
        {
            _isNull = false;
            ID = id;
            imgControl = curImgControl;
            vectorPos = curVectorPos;
            rectControl = curRect;

            strControlHeader = curControlHeader;
            strControlText = curControlText;


            menuButtons = curMenuButtons;
            menuRadioButtonControls = curMenuRadioButtonControls;
        }



        //Draw Commands

        public void Draw(SpriteBatch spriteBatch, Model.StaticFonts staticFonts)
        {
            //Display Menu Image
            spriteBatch.Draw(this.imgControl, this.rectControl, Color.White);

            //Display Menu Header
            Vector2 textVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 10);
            spriteBatch.DrawString(staticFonts._courierNew, this.strControlHeader, textVector, Color.Black);
        }

    }
}

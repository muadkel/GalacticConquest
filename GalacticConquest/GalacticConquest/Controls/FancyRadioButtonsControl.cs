using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GalacticConquest.Controls
{
    public class FancyRadioButtonsControl
    {
        public string ID;

        public Dictionary<string, string> strControlText;

        public Texture2D imgControlUnchecked;

        public Texture2D imgControlChecked;

        public float pxlHeightBetweenButtons;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public bool _isNull;

        public int _selectedIndex;

        public string _selectedValue;

        public FancyRadioButtonsControl()
        {
            _isNull = true;
            ID = "";
            _selectedIndex = -1;
            _selectedValue = "";

            vectorPos = Vector2.Zero;

            pxlHeightBetweenButtons = 0;

            strControlText = new Dictionary<string, string>();
        }

        public FancyRadioButtonsControl(string id, Texture2D curImgControlUnchecked, Texture2D curImgControlChecked, float curPxlHeightBetweenButtons, Vector2 curVectorPos, Rectangle curRect, Dictionary<string,string> curControlText)
        {
            _isNull = false;
            ID = id;
            _selectedIndex = -1;
            _selectedValue = "";

            imgControlUnchecked = curImgControlUnchecked;
            imgControlChecked = curImgControlChecked;
            pxlHeightBetweenButtons = curPxlHeightBetweenButtons;

            vectorPos = curVectorPos;
            rectControl = curRect;

            strControlText = curControlText;

        }

        public FancyRadioButtonsControl(string id, Texture2D curImgControlUnchecked, Texture2D curImgControlChecked, float curPxlHeightBetweenButtons, Vector2 curVectorPos, Rectangle curRect, Dictionary<string, string> curControlText, int selectedIndex)
        {
            _isNull = false;
            ID = id;
            _selectedIndex = selectedIndex;

            imgControlUnchecked = curImgControlUnchecked;
            imgControlChecked = curImgControlChecked;
            vectorPos = curVectorPos;
            rectControl = curRect;

            pxlHeightBetweenButtons = curPxlHeightBetweenButtons;

            strControlText = curControlText;

        }






        //Update Methods
        // MouseClicks
        public bool mouseClicked(GameUpdateClassComponents curGameUpdateComponents, float buttonY)
        {//Check the radio button clicked. ButtonY is the Y position of each button
            bool blnReturn = false;

            if (curGameUpdateComponents._curMouseState.X > this.vectorPos.X && curGameUpdateComponents._curMouseState.X < (this.vectorPos.X + this.imgControlUnchecked.Width))
            {
                if (curGameUpdateComponents._curMouseState.Y > buttonY && curGameUpdateComponents._curMouseState.Y < (buttonY + this.imgControlUnchecked.Height))
                {
                    blnReturn = true;
                }
            }

            return blnReturn;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.GameEngineControls
{
    public class ConstructionMenuControl
    {
        public string ID;

        public DrawObj.Sprite menuBackground;


        public string strControlHeader;



        public List<Controls.FancyButtonControl> menuButtons;

        public Controls.FancyDropDownBoxControl destinationDropDown;

        public Controls.FancyDropDownBoxControl facilityTypeDropDown;

        

        public bool _isNull;



        public ConstructionMenuControl()
        {
            ID = "";
            _isNull = true;
            strControlHeader = "";
            menuBackground = new DrawObj.Sprite();

            menuButtons = new List<FancyButtonControl>();
            destinationDropDown = new FancyDropDownBoxControl();
            facilityTypeDropDown = new FancyDropDownBoxControl();
        }


        public ConstructionMenuControl(string id, string _strControlHeader, DrawObj.Sprite _background)
        {
            ID = id;
            strControlHeader = _strControlHeader;
            menuBackground = _background;

            menuButtons = new List<FancyButtonControl>();
            destinationDropDown = new FancyDropDownBoxControl();
            facilityTypeDropDown = new FancyDropDownBoxControl();
        }



        //Draw Commands
        public void Draw(GameDrawClassComponents curGameDrawClassComponents)
        {
            if (!_isNull)
            {
                menuBackground.Draw(curGameDrawClassComponents);


                //Display build time and cost.

                /////Implement later. This is temp gap solution?!?
                if (!curGameDrawClassComponents._staticDataCards._isNull)
                {
                    Vector2 vectManuTime = new Vector2(110, 250);
                    Vector2 vectManuCost = new Vector2(110, 300);

                    string strFacilityTypeName = facilityTypeDropDown.textOptions[facilityTypeDropDown.selectedIndex];

                    DataCards.Facility facilCheck = curGameDrawClassComponents._staticDataCards.getFacilityByName(strFacilityTypeName);

                    string strManuTimeDisplay = "Construction Day(s): " + facilCheck.baseManufactureCost.BaseConstructionTime.ToString();

                    string strManuCostDisplay = "Construction Cost: " + facilCheck.baseManufactureCost.ResourceCost.ToString();

                    curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, strManuTimeDisplay, vectManuTime, Color.Black);

                    curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, strManuCostDisplay, vectManuCost, Color.Black);
                }


                displayButtonsInMenu(curGameDrawClassComponents);



                destinationDropDown.Draw(curGameDrawClassComponents);

                facilityTypeDropDown.Draw(curGameDrawClassComponents);



            }
        }


        public void displayButtonsInMenu(GameDrawClassComponents curGameDrawClassComponents)
        {
            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(curGameDrawClassComponents._spriteBatch, curGameDrawClassComponents._staticFonts);
                }
            }

        }

    }
}

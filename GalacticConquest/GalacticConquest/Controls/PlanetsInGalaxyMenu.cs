using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class PlanetsInGalaxyMenu
    {
        public string ID;

        public string strControlHeader;

        public string strControlText;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public Model.OrientationType menuOrientation;

        public List<Controls.FancyButtonControl> menuButtons;

        public List<Controls.FancyRadioButtonsControl> menuRadioButtonControls;


        public List<DataCards.Planet> menuPlanetControls;

        public bool _isNull;
        public bool _isEnabled;

        public const string LeftMenuTopLeftXY = "X:5,Y:71";
        public const string LeftMenuBottomRightXY = "X:516,Y:717";

        public const string RightMenuTopLeftXY = "";
        public const string RightMenuBottomRightXY = "";


        public PlanetsInGalaxyMenu()
        {
            _isNull = true;
            _isEnabled = false;
            ID = "";
            vectorPos = Vector2.Zero;
            menuOrientation = Model.OrientationType.Left;
            menuButtons = new List<FancyButtonControl>();
            menuRadioButtonControls = new List<FancyRadioButtonsControl>();

        }

        public PlanetsInGalaxyMenu(string id, Texture2D curImgControl, Model.OrientationType curOrientation, string curControlHeader, string curControlText, List<Controls.FancyButtonControl> curMenuButtons)
        {
            _isNull = false;
            _isEnabled = true;
            ID = id;
            imgControl = curImgControl;
            
            //rectControl = curRect;
            menuOrientation = curOrientation;

            strControlHeader = curControlHeader;
            strControlText = curControlText;


            menuButtons = curMenuButtons;
            menuRadioButtonControls = new List<FancyRadioButtonsControl>();


            setMenuVector();

        }

        public void setMenuVector()
        {
            setMenuVector(menuOrientation);
        }

        public void setMenuVector(Model.OrientationType curOrientation)
        {
            if (curOrientation == GalacticConquest.Model.OrientationType.Left)
            {
                Model.XYStringToInt curTL = new Model.XYStringToInt(LeftMenuTopLeftXY);
                vectorPos = new Vector2(curTL.X, curTL.Y);
            }
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
            curGameDrawComponents._spriteBatch.Draw(this.imgControl, this.vectorPos, Color.White);


            //Display Menu Header
            Vector2 textVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 10);
            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, this.strControlHeader, textVector, Color.Black);



            Vector2 firstPlanetVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 50);

            //Display Planets
            foreach (DataCards.Planet curPlanet in this.menuPlanetControls)
            {
                string planetImagePath = Model.DataUtilities._PlanetImagePath + curPlanet.Name + "50x50.bmp";

                Texture2D curPlanetImg = Texture2D.FromFile(curGameDrawComponents._graphicsDevice, planetImagePath);
                //Display Planet Image
                curGameDrawComponents._spriteBatch.Draw(curPlanetImg, firstPlanetVector, Color.White);

                Vector2 planetHeaderTextVector = new Vector2();
                planetHeaderTextVector.X = firstPlanetVector.X + 70;
                planetHeaderTextVector.Y = firstPlanetVector.Y;
                //Displat Planet Header text
                curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, curPlanet.HeaderText, planetHeaderTextVector, Color.Black);

                //Display Planet Buttons
                string infoBtnImagePath = Model.DataUtilities._ButtonsImagePath + "30x30InfoButton.bmp";

                Texture2D curInfoBtnImg = Texture2D.FromFile(curGameDrawComponents._graphicsDevice, infoBtnImagePath);

                planetHeaderTextVector.X += 275;

                curGameDrawComponents._spriteBatch.Draw(curInfoBtnImg, planetHeaderTextVector, Color.White);

                string commandBtnImagePath = Model.DataUtilities._ButtonsImagePath + "30x30CommandButton.bmp";

                Texture2D curCommandBtnImg = Texture2D.FromFile(curGameDrawComponents._graphicsDevice, commandBtnImagePath);

                planetHeaderTextVector.X += 50;


                curGameDrawComponents._spriteBatch.Draw(curCommandBtnImg, planetHeaderTextVector, Color.White);


                string orbitBtnImagePath = Model.DataUtilities._ButtonsImagePath + "26x28ShipsButton.bmp";

                Texture2D curOrbitBtnImg = Texture2D.FromFile(curGameDrawComponents._graphicsDevice, orbitBtnImagePath);

                planetHeaderTextVector.X += 50;


                curGameDrawComponents._spriteBatch.Draw(curOrbitBtnImg, planetHeaderTextVector, Color.White);


                firstPlanetVector.Y += 60;
            }


            //Display Menu Buttons
            displayButtonsInMenu(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts, this.menuButtons);
        }

        public void displayButtonsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyButtonControl> menuButtons)
        {
            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(spriteBatch, staticFonts);
                }
            }

        }


    }
}

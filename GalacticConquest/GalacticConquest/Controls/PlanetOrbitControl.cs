using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class PlanetOrbitControl
    {
        public string ID;

        public string strControlHeader;

        public string strControlText;

        public string strPlanetName;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public Model.OrientationType menuOrientation;

        public List<Controls.FancyButtonControl> menuButtons;

        public List<Controls.FancyDropDownBoxControl> menuDropDownBoxControls;


        public List<GameEngine.PlayerInvObjs.InvUnit> menuInvUnits;

        public bool _isNull;
        public bool _isEnabled;

        public const string LeftMenuTopLeftXY = "X:5,Y:71";
        public const string LeftMenuBottomRightXY = "X:516,Y:717";

        public const string RightMenuTopLeftXY = "";
        public const string RightMenuBottomRightXY = "";


        public PlanetOrbitControl()
        {
            _isNull = true;
            _isEnabled = false;
            ID = "";
            strPlanetName = "";
            vectorPos = Vector2.Zero;
            menuOrientation = Model.OrientationType.Left;
            menuButtons = new List<FancyButtonControl>();
            menuDropDownBoxControls = new List<FancyDropDownBoxControl>();

        }

        public PlanetOrbitControl(string id, string planetName, Texture2D curImgControl, Model.OrientationType curOrientation, string curControlHeader, string curControlText, List<Controls.FancyButtonControl> curMenuButtons, List<DataCards.Planet> planetList, GameUpdateClassComponents curGameDrawClassComponents, List<GameEngine.PlayerInvObjs.InvUnit> InvObjs)
        {
            _isNull = false;
            _isEnabled = true;
            ID = id;
            strPlanetName = planetName;
            imgControl = curImgControl;
            
            //rectControl = curRect;
            menuOrientation = curOrientation;

            strControlHeader = curControlHeader;
            strControlText = curControlText;


            menuInvUnits = InvObjs;

            menuButtons = curMenuButtons;
            menuDropDownBoxControls = new List<Controls.FancyDropDownBoxControl>();




            setMenuVector();



            Vector2 firstPlanetVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 50);

            int i = 1;

            foreach (GameEngine.PlayerInvObjs.InvUnit curInvUnit in this.menuInvUnits)
            {

                int myId = curInvUnit.id;

                Vector2 ddlVector = new Vector2();
                ddlVector.X = firstPlanetVector.X + 170;
                ddlVector.Y = firstPlanetVector.Y;

                Controls.FancyDropDownBoxControl myfancyDDL = new FancyDropDownBoxControl();
                myfancyDDL._isEnabled = true;

                myfancyDDL.dropDownBoxRect = new Rectangle((int)ddlVector.X, (int)ddlVector.Y, 125, 25);

                myfancyDDL.backColor = Color.Red;
                myfancyDDL.borderColor = Color.White;
                myfancyDDL.borderSize = 2;
                myfancyDDL.dropDownBoxArrow = new DrawObj.Sprite("mdDDLSprite", ddlVector, curGameDrawClassComponents._staticTextureImages._dropDownArrowTexture);
                myfancyDDL.dropDownCollapsed = true;
                myfancyDDL.selectedIndex = 0;
                myfancyDDL.ID = "ddlDestinationPlanet" + myId;

                List<string> strLstPlanets = new List<string>();

                foreach (DataCards.Planet curPlanet in planetList)
                {
                    strLstPlanets.Add(curPlanet.Name);
                }

                myfancyDDL.textOptions = strLstPlanets;


                menuDropDownBoxControls.Add(myfancyDDL);
                string planetImagePath = "";


                if (System.IO.File.Exists(Model.DataUtilities._ShipsImagePath + curInvUnit.iuShip.Name + ".jpg"))
                {
                    planetImagePath = Model.DataUtilities._ShipsImagePath + curInvUnit.iuShip.Name + ".jpg";

                }
                else
                {
                    planetImagePath = Model.DataUtilities._ShipsImagePath + "UnknownShipImage.jpg";
                    
                }

                Texture2D curPlanetImg = Texture2D.FromFile(curGameDrawClassComponents._graphicsDevice, planetImagePath);



                string orbitBtnImagePath = Model.DataUtilities._ButtonsImagePath + "30x30DayAdvanceButton.bmp";

                Texture2D curOrbitBtnImg = Texture2D.FromFile(curGameDrawClassComponents._graphicsDevice, orbitBtnImagePath);

                ddlVector.X += 250;





                //urGameDrawClassComponents._spriteBatch.Draw(curOrbitBtnImg, ddlVector, Color.White);
                this.menuButtons.Add(new Controls.FancyButtonControl("btnSendShip" + myId, curGameDrawClassComponents._staticTextureImages._buttonTexture, ddlVector, new Rectangle((int)ddlVector.X, (int)ddlVector.Y, 30, 50), "Go"));


                
                



                firstPlanetVector.Y += curPlanetImg.Height + 60;
                i++;
            }

        }

        public string getDestinationPlanetNameByID(int invId)
        {
            string rtnPlanetName = "";

            foreach (Controls.FancyDropDownBoxControl curDDL in menuDropDownBoxControls)
            {

                int invUnitID = Convert.ToInt32(curDDL.ID.Replace("ddlDestinationPlanet", ""));

                if(invId == invUnitID)
                {
                    rtnPlanetName = curDDL.textOptions[curDDL.selectedIndex];
                }

            }


            return rtnPlanetName;
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

        public void Draw(GameDrawClassComponents curGameDrawClassComponents, List<DataCards.Planet> planetList)
        {
            curGameDrawClassComponents._spriteBatch.Draw(this.imgControl, this.vectorPos, Color.White);


            //Display Menu Header
            Vector2 textVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 10);
            curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, this.strControlHeader, textVector, Color.Black);



            Vector2 firstPlanetVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 50);

            int i = 0;

            //Display Planets
            foreach (GameEngine.PlayerInvObjs.InvUnit curInvUnit in this.menuInvUnits)
            {
                string planetImagePath = "";

                if (curInvUnit.transitObj._inTransit)
                {
                    planetImagePath = Model.DataUtilities._ShipsImagePath + "Hyper.jpg";
                }
                else
                {
                    if(System.IO.File.Exists(Model.DataUtilities._ShipsImagePath + curInvUnit.iuShip.Name + ".jpg"))
                        planetImagePath = Model.DataUtilities._ShipsImagePath + curInvUnit.iuShip.Name + ".jpg";
                    else
                        planetImagePath = Model.DataUtilities._ShipsImagePath + "UnknownShipImage.jpg";
                }


                Texture2D curPlanetImg = Texture2D.FromFile(curGameDrawClassComponents._graphicsDevice, planetImagePath);
                //Display Planet Image
                curGameDrawClassComponents._spriteBatch.Draw(curPlanetImg, firstPlanetVector, Color.White);

                Vector2 planetHeaderTextVector = new Vector2();
                planetHeaderTextVector.X = firstPlanetVector.X + 70;
                planetHeaderTextVector.Y = firstPlanetVector.Y;
                //Displat Planet Header text
                curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, curInvUnit.iuShip.HeaderText, planetHeaderTextVector, Color.Black);


                Vector2 ddlVector = new Vector2();
                ddlVector.X = firstPlanetVector.X + 170;
                ddlVector.Y = firstPlanetVector.Y;

                Controls.FancyDropDownBoxControl myfancyDDL = menuDropDownBoxControls[i];
                //myfancyDDL._isEnabled = true;

                //myfancyDDL.dropDownBoxRect = new Rectangle((int)ddlVector.X, (int)ddlVector.Y, 125, 50);
                
                //myfancyDDL.backColor = Color.Red;
                //myfancyDDL.borderColor = Color.White;
                //myfancyDDL.borderSize = 2;
                //myfancyDDL.dropDownBoxArrow = new DrawObj.Sprite("mdDDLSprite", ddlVector, curGameDrawClassComponents._staticTextureImages._dropDownArrowTexture);
                //myfancyDDL.dropDownCollapsed = true;
                //myfancyDDL.selectedIndex = 0;


                //menuDropDownBoxControls.Add(myfancyDDL);

                List<string> strLstPlanets = new List<string>();

                foreach (DataCards.Planet curPlanet in planetList)
                {
                    strLstPlanets.Add(curPlanet.Name);
                }

                //myfancyDDL.textOptions = strLstPlanets;




                myfancyDDL.Draw(curGameDrawClassComponents);
                
                
                //Displat Planet Header text
                //curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, curInvUnit.iuShip.HeaderText, planetHeaderTextVector, Color.Black);

                
                i++;

                firstPlanetVector.Y += curPlanetImg.Height + 60;
            }
            

            //Display Menu Buttons
            displayButtonsInMenu(curGameDrawClassComponents._spriteBatch, curGameDrawClassComponents._staticFonts, this.menuButtons);
        }

        public void displayButtonsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyButtonControl> menuButtons)
        {
            //Vector2 firstPlanetVector = new Vector2(this.vectorPos.X + 10, this.vectorPos.Y + 50);

            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    //curButton.vectorPos.Y = firstPlanetVector.Y;
                    curButton.Draw(spriteBatch, staticFonts);
                }

                //firstPlanetVector.Y += 160;
            }

        }

    }
}

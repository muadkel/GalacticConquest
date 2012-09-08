using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class PlanetCommandControl : IDrawable
    {
        public string Name;

        public string HeaderText;

        public string Description;


        public string SelectedPlanetName;

        //public Model.DataCardType dataCardType;

        //public Texture2D planetImageToDisplay;


        public Texture2D menuImageToDisplay;
        public Vector2 menuVector;

        public object tag;
        //public Texture2D imageOfDataTypeToDisplay;

        
        public string imgDTTopLeftXY = "X:130,Y:160";


        //public string imgDescTopLeftXY = "X:302,Y:166";
        //public string LeftMenuTopLeftXY = "X:5,Y:71";

        public Controls.TabPanel.TabPanelControl tabControl;


        public List<Controls.FancyButtonControl> menuButtons;

        public bool _isNull;
        public bool _isEnabled;

        public PlanetCommandControl()
        {
            _isNull = true;
            _isEnabled = false;

            HeaderText = "";
            Description = "";

            SelectedPlanetName = "";

            menuVector = new Vector2();

            //dataCardType = GalacticConquest.Model.DataCardType.Unknown;

            tabControl = new Controls.TabPanel.TabPanelControl();

            menuButtons = new List<FancyButtonControl>();
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

        //public GameInformationMenu()
        //{
        //    _isNull = true;
        //    _isEnabled = false;

        //    HeaderText = "";
        //    Description = "";

        //    menuVector = new Vector2();

        //    //dataCardType = GalacticConquest.Model.DataCardType.
        //}


        //Update Methods
        public void CheckMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {
            //Check Tab Clicks
            tabControl.CheckMouseClick(curGameUpdateComponents);


        }
        
        
        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {
            curGameDrawComponents._spriteBatch.Draw(this.menuImageToDisplay, this.menuVector, Color.White);


            //Display Menu Header
            Vector2 textVector = new Vector2(this.menuVector.X + 10, this.menuVector.Y + 10);
            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, this.HeaderText, textVector, Color.Black);

            

            DataCards.Planet curDataCard = (DataCards.Planet)this.tag;

            Vector2 planetImgVector = new Vector2(menuVector.X + 10, menuVector.Y + 65);
            Texture2D planetImg = Texture2D.FromFile(curGameDrawComponents._graphicsDevice, Model.DataUtilities._PlanetImagePath + curDataCard.Name + ".bmp");

            curGameDrawComponents._spriteBatch.Draw(planetImg, planetImgVector, Color.White);


            Vector2 groundSlotsDisplay = new Vector2(planetImgVector.X, planetImgVector.Y + planetImg.Height + 10);
            string textToDisplay = "Owner:\n" + curDataCard.Owner.Name;
            textToDisplay += "\n\nGround Slots:\n" + curDataCard.GroundSpaces.Facilities.Count.ToString() + "\\" + curDataCard.GroundSpaces.AllSpaces.ToString();
            textToDisplay += "\n\nFleets In Orbit:\n" + curDataCard.Orbit.StarshipFleetsInOrbit.Count.ToString();
            textToDisplay += "\n\nResources Per Day:\n" + curDataCard.RawMaterialsPerDay.ToString();
            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, textToDisplay, groundSlotsDisplay, Color.Black);

            //Display Menu Buttons
            displayButtonsInMenu(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts, this.menuButtons);



            //Display Tab Control
            tabControl.Draw(curGameDrawComponents);
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

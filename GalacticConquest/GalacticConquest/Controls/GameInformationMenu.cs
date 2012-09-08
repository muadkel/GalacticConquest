using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class GameInformationMenu
    {
        public string Name;

        public string HeaderText;

        public string Description;

        public Model.DataCardType dataCardType;

        public Texture2D imageToDisplay;
        
        public Vector2 menuVector;


        public Texture2D imageOfDataTypeToDisplay;

        
        public string imgDTTopLeftXY = "X:130,Y:160";


        public string imgDescTopLeftXY = "X:302,Y:166";
        //public string LeftMenuTopLeftXY = "X:5,Y:71";

        public object tag;


        public Controls.ScrollPanel.ScrollPanelTextControl scrlPnlDescriptionText;


        public List<Controls.FancyButtonControl> menuButtons;

        public bool _isNull;
        public bool _isEnabled;

        public GameInformationMenu()
        {
            _isNull = true;
            _isEnabled = false;

            HeaderText = "";
            Description = "";

            menuVector = new Vector2();

            dataCardType = GalacticConquest.Model.DataCardType.Unknown;

            menuButtons = new List<FancyButtonControl>();

            scrlPnlDescriptionText = new Controls.ScrollPanel.ScrollPanelTextControl();

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





        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {
            curGameDrawComponents._spriteBatch.Draw(this.imageToDisplay, this.menuVector, Color.White);


            //Display Menu Header
            Vector2 textVector = new Vector2(this.menuVector.X + 10, this.menuVector.Y + 10);
            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, this.HeaderText, textVector, Color.Black);





            //Vector2 firstPlanetVector = new Vector2(LeftGPMenu.vectorPos.X + 10, LeftGPMenu.vectorPos.Y + 50);

            //Display Image


            //curInfoMenu.
            Model.XYStringToInt curTopLeft = new Model.XYStringToInt(this.imgDTTopLeftXY);
            DataCards.Planet curDataCard = (DataCards.Planet)this.tag;
            curGameDrawComponents._spriteBatch.Draw(Texture2D.FromFile(curGameDrawComponents._graphicsDevice, Model.DataUtilities._PlanetImagePath + curDataCard.Name + ".bmp"), new Vector2(curTopLeft.X, curTopLeft.Y), Color.White);

            //ScrollPanel
            scrlPnlDescriptionText.scrlPnlText = this.Description;
            scrlPnlDescriptionText.Draw(curGameDrawComponents);


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

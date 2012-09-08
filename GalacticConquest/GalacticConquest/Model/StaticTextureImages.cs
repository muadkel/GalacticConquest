using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Model
{
    public class StaticTextureImages
    {
        //Public variables
        public Texture2D _infoMenu;
        public Texture2D _galacticPlanetView;
        public Texture2D _galacticMap;

        public Texture2D _gameTopBar;
        public Texture2D _gameTopBarDayAdvance;

        public Texture2D _gameRightBar;

        public Texture2D _buttonTexture;

        public Texture2D _imgPopMessage;

        public Texture2D _imgBackground;

        public Texture2D _imgMenu;


        public Texture2D _tabSelected;
        public Texture2D _tabUnselected;

        public Texture2D _upDownArrowTexture;

        public Texture2D _dropDownArrowTexture;


        public Texture2D _scrollingPanel;


        public Texture2D _planetCommandMenu;
        public Texture2D _orbitCommandMenu;

        public Texture2D _constructionMenu;


        public Texture2D _underConstructionFacility;

        public StaticTextureImages(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            //_imgBackground = Content.Load<Texture2D>("StartScreen");
            _infoMenu = Content.Load<Texture2D>("InfoMenu");
            
            _galacticPlanetView = Content.Load<Texture2D>("GalacticPlanetView");

            _galacticMap = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._GameControlsImagePath + "GalacticMap.png");

            _gameTopBar = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._GameControlsImagePath + "GameTopBar.png");
            _gameTopBarDayAdvance = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._ButtonsImagePath + "30x30DayAdvanceButton.bmp");
            _gameRightBar = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._GameControlsImagePath + "GameRightBar.png");

            _buttonTexture = Content.Load<Texture2D>("130x150_Button");

            //_galacticMap.
            _imgPopMessage = Content.Load<Texture2D>("MessageBox_163x100");


            _imgMenu = Content.Load<Texture2D>("300x300_Window");


            _tabSelected = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._ControlsImagePath + "TabSelected_125_40.jpg");
            _tabUnselected = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._ControlsImagePath + "TabUnselected_125_40.jpg");

            
            _planetCommandMenu = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._MsgTemplatesImagePath + "PlanetCommandMenu.bmp");
            _orbitCommandMenu = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._MsgTemplatesImagePath + "413x500_Orbit_Window.bmp");


            _constructionMenu = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._MsgTemplatesImagePath + "ConstructionMenu_400x300.bmp");

            //ConstructionMenu_400x300.bmp

            _scrollingPanel = Content.Load<Texture2D>("ScrollingPanel");

            //Get Button Image
            //curButtonTexture = Content.Load<Texture2D>("130x150_Button");

            _dropDownArrowTexture = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._ButtonsImagePath + "30x30DropDownArrowButton.bmp");

            _underConstructionFacility = Texture2D.FromFile(graphicsDevice, Model.DataUtilities._FacilitiesImagePath + "UnderConstruction.jpg");
            
        }
    }
}

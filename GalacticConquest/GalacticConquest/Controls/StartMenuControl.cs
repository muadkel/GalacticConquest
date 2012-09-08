using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class StartMenuControl
    {
        //Game Components - Draw, Mouse, Static Content
        public GameUpdateClassComponents curGameComponents;

        //Text
        public string strHeaderText;
        public Vector2 headerVector;

        //Image Background
        public Texture2D backgroundTexture;
        public Vector2 backgroundVector;
        public SpriteFont headerFont;

        //Controls
        public List<Controls.FancyButtonControl> menuButtons;

        //Attributes
        public bool _isEnabled;
        public bool _isVisible;

        //Shell of the obj
        public StartMenuControl()
        {
            curGameComponents = new GameUpdateClassComponents();
            strHeaderText = "";
            _isEnabled = false;
            _isVisible = false;
            menuButtons = new List<FancyButtonControl>();
        }

        //Create the Start Menu
        public StartMenuControl(string curHeaderText, SpriteFont curHeaderFont, Vector2 curHeaderVector, Texture2D curBackgroundTexture, Vector2 curBackgroundVector, List<Controls.FancyButtonControl> curMenuButtons)
        {
            curGameComponents = new GameUpdateClassComponents();

            //_isEnabled = true;
            strHeaderText = curHeaderText;
            headerFont = curHeaderFont;
            headerVector = curHeaderVector;
            backgroundTexture = curBackgroundTexture;
            backgroundVector = curBackgroundVector;
            menuButtons = curMenuButtons;
        }


        //Methods

        public void Show()
        {
            _isEnabled = true;
            _isVisible = true;
        }


        public void Hide()
        {
            _isEnabled = false;
            _isVisible = false;
        }




        //Draw Commands

        public void Draw(SpriteBatch spriteBatch, Model.StaticFonts staticFonts)
        {

        }

    }
}
